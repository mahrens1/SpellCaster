using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float curHealth;
    public int damage;
    private Vector2 input;

    private Rigidbody rb;
    private Animator animator;
    private readonly int movementAnimParam = Animator.StringToHash("movementInput");

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        damage = 1;
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

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        movementInput();
    }

    public void TakeDamage(float amount)
    {
        curHealth -= amount;
    }

    public float DealDamageToPlayer(int playerHealth)
    {
        playerHealth -= damage;

        return playerHealth;
    }

    public void movementInput()
    {
        animator.SetFloat(movementAnimParam, input.magnitude);
    }
}
