using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float curHealth;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
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

}
