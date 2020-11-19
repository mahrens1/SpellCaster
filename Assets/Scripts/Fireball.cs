using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float m_Speed = 130f;
    [SerializeField] private float m_Lifespan = 3f;
    [SerializeField] float damage;

    private Rigidbody m_Rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody.AddForce(transform.forward * m_Speed, ForceMode.Impulse);
        Destroy(gameObject, m_Lifespan);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            var enemy = other.GetComponent<Enemy>();

            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("damageables"))
        {
            var damageable = other.GetComponent<Damageable>();

            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
