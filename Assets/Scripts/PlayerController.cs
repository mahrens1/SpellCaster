﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpForce = 10f;

    private Rigidbody playerRb;
    private Vector2 input;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var moveDirecton = new Vector2(input.x, input.y);

        playerRb.AddForce(moveDirecton * speed);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        //Shoot
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
        }
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }
}

