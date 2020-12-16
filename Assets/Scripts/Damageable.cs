using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] ParticleSystem destroyedEffect;

    void Update()
    {
        if (health <= 0)
        {
            Exploed();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    public void Exploed()
    {
        ParticleSystem explosionEffect = Instantiate(destroyedEffect);

        explosionEffect.transform.position = transform.position;

        explosionEffect.loop = false;
        explosionEffect.Play();

        Destroy(explosionEffect.gameObject, explosionEffect.duration);

        Destroy(gameObject);
    }
}
