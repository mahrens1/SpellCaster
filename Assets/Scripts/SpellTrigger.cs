using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public enum SpellState { fireball, lightningbolt}

public class SpellTrigger : MonoBehaviour
{
    public GameObject m_Projectile;    
    public Transform m_SpawnTransform;
    public bool canShoot;

    public SpellState State;

    private void Start()
    {
        canShoot = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canShoot)
            {
                canShoot = false;
                StartCoroutine(Attack(3));
            }

        }
    }

    public IEnumerator Attack(float firerate)
    {    
        Instantiate(m_Projectile, m_SpawnTransform.position, m_SpawnTransform.rotation);
        yield return StartCoroutine(WaitForCooldown(firerate));
        canShoot = true;
    }

    public IEnumerator WaitForCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
    }
}
