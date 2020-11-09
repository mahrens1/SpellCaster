using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    [SerializeField] EnemysClearedTrigger trigger;

    // Update is called once per frame
    void Update()
    {
        if (!trigger.enemiesPresent)
        {
            Destroy(gameObject);
        }   
    }
}
