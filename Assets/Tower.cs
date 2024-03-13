using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public GameObject projectilePrefab;
    public Vector3 SpawnPosition;

    public float lastSpawnTime, spawnDelay = 1.0f;

    void Update()
    {
        if (isActiveAndEnabled)
        {
            SpawnProjectile();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + SpawnPosition, 0.5f);
        Gizmos.DrawRay(transform.position + SpawnPosition, transform.rotation * Vector3.forward * 0.75f);
    }

    public void SpawnProjectile()
    {
        if (lastSpawnTime + spawnDelay < Time.time)
        {
            lastSpawnTime = Time.time;
            GameObject newObject = Instantiate(projectilePrefab, transform.position + SpawnPosition, transform.rotation, transform);
            newObject.GetComponent<SeekingProjectile>().direction = transform.forward;
        }
    }
}
