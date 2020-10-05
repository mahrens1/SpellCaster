using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float speed = 10;

    private Vector2 playerInput;

    private Rigidbody characterRig;
    private Collider characterCollider;


    void Start()
    {
        characterRig = GetComponent<Rigidbody>();
        characterCollider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        var inputDirection = new Vector3(playerInput.x, 0, playerInput.y);

        if (Input.GetKeyDown(KeyCode.A))
        {
            inputDirection.x = speed;
        }

        characterRig.AddForce((inputDirection * Time.deltaTime) , ForceMode.Impulse);
    }

    void Update()
    {
        playerInput.x = Input.GetAxisRaw("Horizontal");
        playerInput.y = Input.GetAxisRaw("Vertical");
    }

    //Methods
    void LeftMovement()
    {
        characterRig.AddForce(new Vector3(speed, 0, 0), ForceMode.Impulse);
    }

    void RightMovement()
    {

    }

    void Jump()
    {

    }

    void ShootSpell()
    {

    }
}
