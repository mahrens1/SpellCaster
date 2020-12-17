using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldFire : MonoBehaviour
{

    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            var player = other.gameObject.GetComponent<PlayerControllerV2>();

            player.curHealth -= damage;
        }
    }
}
