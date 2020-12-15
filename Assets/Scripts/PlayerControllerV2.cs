using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool isDead;
    private Animator animator;
    private readonly int movementAnimParam = Animator.StringToHash("movementInput");
    private readonly int jumpAnimParam = Animator.StringToHash("jumpInput");
    private readonly int deathAnimParam = Animator.StringToHash("playerHealth");

    [SerializeField] [Tooltip("0 = No Turning, 1 = Instant Snap")] [Range(0, 1)] private float turnspeed = 0.1f;


    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();

        Cursor.visible = false;

        curHealth = 5;

        isOnGround = true;
        isDead = false;
    }

    private void FixedUpdate()
    {
        Vector3 cameraRelativeInputDirection = GetCameraRelativeInputDirection();

        UpdatePhysicsMaterial();

        Move(cameraRelativeInputDirection);

        RotateToFaceInputDirection(cameraRelativeInputDirection);

        //Jump
        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            isOnGround = false;
            playerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        //JumpInput();

    }

    /// <summary>
    /// Turning the character to face the direction it wants to move in.
    /// </summary>
    /// <param name="movementDirection"> The direction the character is trying to move in. </param>
    private void RotateToFaceInputDirection(Vector3 movementDirection)
    {
        if (movementDirection.magnitude > 0)
        {
            var targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnspeed);
        }
    }

    /// <summary>
    /// Moves the player in a direction based on its max speed.
    /// </summary>
    /// <param name="moveDirection"> The direction to move in. </param>
    private void Move(Vector3 moveDirection)
    {
        if (playerRb.velocity.magnitude < maxSpeed)
        {
            playerRb.AddForce(moveDirection * accelerationForce, ForceMode.Acceleration);
        }
    }

    /// <summary>
    /// Updates physics material to a low friction option if player is moving 
    /// or a high friction option if trying to stop.
    /// </summary>
    private void UpdatePhysicsMaterial()
    {
        collider.material = input.magnitude > 0 ? movingPhysicsMaterial : stoppingPhysicsMaterial;
    }

    /// <summary>
    /// Uses the input of Vector to create camera relative version so the player can move based on the camera.
    /// </summary>
    /// <returns> cameraRelativeInputDirection. </returns>
    private Vector3 GetCameraRelativeInputDirection()
    {
        var inputDirection = new Vector3(input.x, 0, input.y);

        Vector3 cameraFlattenedForward = Camera.main.transform.forward;
        cameraFlattenedForward.y = 0;
        var cameraRotation = Quaternion.LookRotation(cameraFlattenedForward);

        Vector3 cameraRelativeInputDirection = cameraRotation * inputDirection;

        return cameraRelativeInputDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.other.gameObject.tag == "ground")
        {
            isOnGround = true;
        }

        if (collision.other.gameObject.tag == "enemy")
        {
            var enemy = collision.other.gameObject.GetComponent<Enemy>();

            curHealth = (int)enemy.DealDamageToPlayer(curHealth);
        }
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        movementInput();
        DeathInput();
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
        animator.SetFloat(jumpAnimParam, input.y);
    }

    public void DeathInput()
    {
        animator.SetInteger(deathAnimParam, curHealth);
    }

    public void CheckDeath()
    {
        if (curHealth <= 0)
        {
            var playerscript = gameObject.GetComponent<PlayerControllerV2>();
            playerscript.enabled = false;

            DeathAnimation();

            if (isDead == true)
            {
                SceneManager.LoadScene("Mainworld");
            }

        }
    }

    public IEnumerator DeathAnimation()
    {
        yield return StartCoroutine(WaitForSeconds(10));
    }

    public IEnumerator WaitForSeconds(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
    }
}
