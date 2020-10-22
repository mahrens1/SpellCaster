using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SpellTrigger : MonoBehaviour
{
    public GameObject m_Projectile;    
    public Transform m_SpawnTransform;
    public IEnumerator coroutine;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Attack()
    {    
        Instantiate(m_Projectile, m_SpawnTransform.position, m_SpawnTransform.rotation);
        yield return StartCoroutine(WaitForCooldown(3));
    }

    public IEnumerator WaitForCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
    }
}
