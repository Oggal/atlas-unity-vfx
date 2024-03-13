using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCreep : MonoBehaviour
{
    public Vector3 Direction = Vector3.back;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Direction * Time.deltaTime;
        if(transform.position.z < -20)
        {
            Destroy(gameObject);
        }
    }
}
