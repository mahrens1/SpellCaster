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
    public float curHealth;

    [SerializeField] [Tooltip("0 = No Turning, 1 = Instant Snap")] [Range(0, 1)] private float turnspeed = 0.1f;
    

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        isOnGround = true;
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
        if(collision.other.gameObject.tag == "ground")
        {
            isOnGround = true;
        }    
        
        if(collision.other.gameObject.tag == "enemy")
        {
            var enemy = collision.other.gameObject.GetComponent<Enemy>();

            curHealth = enemy.DealDamageToPlayer(curHealth);
        }
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }
}
