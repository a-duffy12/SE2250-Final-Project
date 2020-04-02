using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public Transform Spawnpoint;
    public GameObject Boss;

    private bool _hasSpawned = false;

    void OnTriggerEnter()
    {
        if (!_hasSpawned) {
            
            _hasSpawned = true;
            Instantiate(Boss, Spawnpoint.position, Spawnpoint.rotation);
        }
        
    }

}
