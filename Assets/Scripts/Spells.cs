using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    [SerializeField] private float m_Speed = 130f;
    [SerializeField] private float m_Lifespan = 3f;
    [SerializeField] float damage;

    private Rigidbody m_Rigidbody;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        m_Rigidbody.AddForce(transform.forward * m_Speed, ForceMode.Impulse);
        Destroy(gameObject, m_Lifespan);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("damageables"))
        {
            var enemy = other.GetComponent<Enemy>();

            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
