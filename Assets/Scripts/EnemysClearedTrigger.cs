using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysClearedTrigger : MonoBehaviour
{

    public bool enemiesPresent;

    public void Start()
    {
        enemiesPresent = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("enemy"))
        {
            enemiesPresent = false;
        }
    }
}
