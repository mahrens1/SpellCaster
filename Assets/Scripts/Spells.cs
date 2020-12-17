using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    [SerializeField] private float m_Speed = 130f;
    [SerializeField] private float m_Lifespan = 3f;
    [SerializeField] float damage;
    [SerializeField] ParticleSystem effect;

    private Rigidbody m_Rigidbody;

    public void Exploed()
    {
        ParticleSystem explosionEffect = Instantiate(effect);

        explosionEffect.transform.position = transform.position;

        explosionEffect.loop = false;
        explosionEffect.Play();

        Destroy(explosionEffect.gameObject, explosionEffect.duration);

        Destroy(gameObject);
    }

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
        if (other.CompareTag("enemy"))
        {
            var enemy = other.GetComponent<Enemy>();

            enemy.TakeDamage(damage);
            Exploed();
            Destroy(gameObject);
        }
        else if (other.CompareTag("damageables"))
        {
            var damageable = other.GetComponent<Damageable>();

            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (!other.CompareTag("player"))
        {
            Exploed();
            Destroy(gameObject);
        }

    }
}
