using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    [SerializeField] private AudioDrivenEvents audioDataSource;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int numberOfObjects;
    [SerializeField] private GameObject[] elements;
    public float effectScale = 1f;


    // Start is called before the first frame update
    void Start()
    {
        elements = new GameObject[numberOfObjects];
        for(int i = 0; i < numberOfObjects; i++)
        {
            var pos = (i - numberOfObjects / 2) * effectScale;
            GameObject newObject = Instantiate(prefab, this.transform);
            
            newObject.transform.localPosition = new Vector3(pos, 0, 0);
            elements[i] = newObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < numberOfObjects; i++)
        {
            float value = audioDataSource.GetAudioBandBuffer(i);
            elements[i].GetComponent<ParticalControl>()?.SetValue(value);
        }
    }
}
