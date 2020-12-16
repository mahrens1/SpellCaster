using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SpellState { fireball, lightningbolt }

public class SpellTrigger : MonoBehaviour
{
    [SerializeField] int shotCooldown;

    public GameObject m_Projectile;    
    public Transform playerTransform;
    public bool canShoot;

    public List<GameObject> projectileList;

    [SerializeField] Text CooldownText;
    [SerializeField] Sprite fireballImage;
    [SerializeField] Sprite iceshardImage;
    [SerializeField] Image spellImage;

    public SpellState State;

    private void Start()
    {
        canShoot = true;
        State = SpellState.fireball;
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

        if (Input.GetKeyDown(KeyCode.E))
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
                m_Projectile = projectileList[1];
                spellImage.sprite = iceshardImage;
                break;

            case SpellState.lightningbolt:

                State = SpellState.fireball;
                m_Projectile = projectileList[0];
                spellImage.sprite = fireballImage;
                break;
        }
    }

    public IEnumerator Attack()
    {
        Instantiate(m_Projectile, playerTransform.position, playerTransform.rotation);

        yield return StartCoroutine(WaitForCooldown(shotCooldown));
        canShoot = true;
    }

    public IEnumerator WaitForCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
    }

    public void UpdateShotCooldownCanvas(int cooldown)
    {
        CooldownText.text = cooldown.ToString();
    }
}
