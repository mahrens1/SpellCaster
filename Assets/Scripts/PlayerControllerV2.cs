using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour
{
    [SerializeField] private float accelerationForce = 10;
    [SerializeField] private float maxSpeed = 2;
    [SerializeField] private PhysicMaterial stoppingPhysicsMaterial, movingPhysicsMaterial;
    [SerializeField] float jumpForce = 10f;

    private new Rigidbody playerRb;
    private Vector2 input;
    private new Collider collider;
    private bool isOnGround;
    public int curHealth;
    private Animator animator;
    private readonly int movementAnimParam = Animator.StringToHash("movementInput");
    private readonly int jumpAnimParam = Animator.StringToHash("isOnGround");

    [SerializeField] [Tooltip("0 = No Turning, 1 = Instant Snap")] [Range(0, 1)] private float turnspeed = 0.1f;
    

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();

        isOnGround = true;
        curHealth = 5;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        var inputDirection = new Vector3(input.x, 0, input.y);

        Vector3 cameraFlattenedForward = Camera.main.transform.forward;
        cameraFlattenedForward.y = 0;
        var cameraRotation = Quaternion.LookRotation(cameraFlattenedForward);

        Vector3 cameraRelativeInputDirection = cameraRotation * inputDirection;

        collider.material = inputDirection.magnitude > 0 ? movingPhysicsMaterial : stoppingPhysicsMaterial;

        if (playerRb.velocity.magnitude < maxSpeed)
        {
            playerRb.AddForce(cameraRelativeInputDirection * accelerationForce, ForceMode.Acceleration);
        }

        if (cameraRelativeInputDirection.magnitude > 0)
        {
            var targetRotation = Quaternion.LookRotation(cameraRelativeInputDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnspeed);
        }

        //Jump
        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            isOnGround = false;
            playerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("ground"))
        {
            isOnGround = true;
        }    
        
        if(collision.collider.gameObject.CompareTag("enemy"))
        {
            var enemy = collision.collider.gameObject.GetComponent<Enemy>();

            curHealth = (int)enemy.DealDamageToPlayer(curHealth);
        }
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        JumpInput();
        movementInput();
        CheckDeath();
    }

    /// <summary>
    /// Changes the players run and idle animation.
    /// </summary>
    public void movementInput()
    {
        animator.SetFloat(movementAnimParam, input.magnitude);
    }

    /// <summary>
    /// Changes to jump animation.
    /// </summary>
    public void JumpInput()
    {
        animator.SetBool(jumpAnimParam, isOnGround);
    }

    public void CheckDeath()
    {
        if(curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
