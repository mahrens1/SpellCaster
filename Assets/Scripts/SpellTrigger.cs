using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SpellTrigger : MonoBehaviour
{
    public GameObject m_Projectile;    
    public Transform m_SpawnTransform;
    public IEnumerator coroutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Attack(3));
        }
    }

    public IEnumerator Attack(float firerate)
    {    
        Instantiate(m_Projectile, m_SpawnTransform.position, m_SpawnTransform.rotation);
        yield return StartCoroutine(WaitForCooldown(firerate));
    }

    public IEnumerator WaitForCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
    }
}
