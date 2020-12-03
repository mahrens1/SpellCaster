using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float curHealth;
    public float damage;

    private Rigidbody rb;
    private Animator animator;
    private readonly int movementAnimParam = Animator.StringToHash("movementInput");

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Look into using properties.
        //Ask David later.
        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if(curHealth <= 0)
        {
            Destroy(gameObject);
        }

        movementInput();
    }

    public void TakeDamage(float amount)
    {
        curHealth -= amount;
    }

    public float DealDamageToPlayer(float playerHealth)
    {
        playerHealth -= damage;

        return playerHealth;
    }

    public void movementInput()
    {
        var input = new Vector2(rb.velocity.x, rb.velocity.y);
        animator.SetFloat(movementAnimParam, input.magnitude);
    }
}
