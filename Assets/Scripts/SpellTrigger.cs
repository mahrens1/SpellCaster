using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellState { fireball, lightningbolt }

public class SpellTrigger : MonoBehaviour
{
    [SerializeField] int shotCooldown;

    public GameObject m_Projectile;    
    public Transform m_SpawnTransform;
    public bool canShoot;

    public List<GameObject> projectileList;

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
                StartCoroutine(Attack());
            }

        }

        if (Input.GetKey(KeyCode.E))
        {
            SwitchSpellState();
        }

    }

    private void SwitchSpellState()
    {
        switch (State)
        {
            case SpellState.fireball:

                State = SpellState.lightningbolt;
                m_Projectile = projectileList[0];
                break;

            case SpellState.lightningbolt:

                State = SpellState.fireball;
                m_Projectile = projectileList[1];
                break;
        }
    }

    public IEnumerator Attack()
    {    
        Instantiate(m_Projectile, m_SpawnTransform.position, m_SpawnTransform.rotation);
        yield return StartCoroutine(WaitForCooldown(shotCooldown));
        canShoot = true;
    }

    public IEnumerator WaitForCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
    }
}
