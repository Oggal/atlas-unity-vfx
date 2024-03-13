using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayFieldController : MonoBehaviour
{
    int selectedSlot = -1;
    public float spacing = 1.0f;
    public GameObject[] slots;
    public GameObject SlotPrefab;
    public GameObject TowerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SpawnSlots();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            var indexHolder = hit.collider.gameObject.GetComponentInParent<IndexHolder>(true);
            if(indexHolder != null)
            {
                if (selectedSlot == -1)
                {
                    selectedSlot = indexHolder.Index;
                }
                else
                {
                    if (selectedSlot != indexHolder.Index)
                    {
                        var temp = slots[selectedSlot];
                        var index = indexHolder.Index;
                        slots[selectedSlot] = slots[index];
                        slots[index] = temp;
                        slots[selectedSlot].GetComponent<IndexHolder>().Index = selectedSlot;
                        slots[index].GetComponent<IndexHolder>().Index = index;
                        SetPosition(selectedSlot);
                        SetPosition(index);
                    }
                    selectedSlot = -1;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        for(int j = 0; j < 2; j++)
        {
            for(int i = 0; i < 8; i++)
            {
                var xPos = (i * spacing) - (3.5f * spacing);
                var zPos = j * spacing;
                Gizmos.DrawWireCube(transform.position + new Vector3(xPos, 0, zPos), new Vector3(0.5f, 0.5f, 0.5f));
            }
        }
    }

    void SpawnSlots()
    {
        slots = new GameObject[16];
        
        GameObject obj = SlotPrefab;
        for(int j = 0; j < 2; j++)
        {
            for(int i = 0; i < 8; i++)
            {
                var xPos = (i * spacing) - (3.5f * spacing);
                var zPos = j * spacing;
                GameObject newSlot = Instantiate(obj, transform.position + new Vector3(xPos, 0, zPos), Quaternion.identity, transform);
                var indexHolder = newSlot.GetComponent<IndexHolder>();
                indexHolder.Index = i + (j * 8);
                slots[i + (j * 8)] = newSlot;
            }
            obj = TowerPrefab;
        }
    }
    void SetPosition(int index)
    {
        var i = index % 8;
        var j = index / 8;
        var xPos = (i * spacing) - (3.5f * spacing);
        var zPos = j * spacing;
        slots[index].transform.position = transform.position + new Vector3(xPos, 0, zPos);
        var T = slots[index].GetComponent<Tower>();
        if (T != null)
            T.lastSpawnTime = Time.time;
    }
}