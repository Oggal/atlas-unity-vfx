using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSpawnSetup : MonoBehaviour
{
    public GameObject creepSpawnPrefab;
    public AudioDrivenEvents audioDataSource;
    public float spacing = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i< 8; i++)
        {
        var xPos = (i * spacing) - (3.5f * spacing);
        GameObject newObject = Instantiate(creepSpawnPrefab, this.transform);
        newObject.transform.localPosition = new Vector3(xPos, 0, 0);
        audioDataSource.audioBandPulseStart[i].AddListener((unusedFloat) => {newObject.GetComponent<ObjectSpawner>().SpawnObject();});
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
