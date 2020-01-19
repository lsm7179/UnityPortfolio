using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTime : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.name.Equals("SkelSpawnTime"))
            {
                EnemyManager.Instance.StartCoroutine("CreateSkelOnce");
            }else if (gameObject.name.Equals("SlimSpawnTime"))
            {
                SlimeManager.Instance.StartCoroutine("CreateSlimeOnce");
            }
            Destroy(this);
        }
    }

}
