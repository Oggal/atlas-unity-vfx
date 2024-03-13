using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Vector3 spawnPosition;
    public float direction = 0.0f, spawnDelay = 1.0f;
    float lastSpawnTime = 0.0f;

    public void SpawnObject()
    {
        if(lastSpawnTime + spawnDelay < Time.time)
        {
            lastSpawnTime = Time.time;
            GameObject newObject = Instantiate(objectToSpawn, transform.position + spawnPosition, Quaternion.Euler(0, direction, 0), transform);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + spawnPosition, 0.5f);
        Gizmos.DrawRay(transform.position + spawnPosition, Quaternion.Euler(0, direction, 0) * Vector3.forward * 0.75f);
    }
}
