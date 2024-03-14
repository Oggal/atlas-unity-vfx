using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SeekingProjectile : MonoBehaviour
{

    public GameObject target;
    float range = 2f;
    public float speed = 0.5f;
    public float damage = 1f;
    public float lifeTime = 5f;
    public Vector3 direction = Vector3.forward;
    public LayerMask mask;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("DestroySelf", lifeTime);
        rb.isKinematic = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
            FindTarget();
        SeekTarget();
        Move();
    }

    void FindTarget()
    {;
        var targets = Physics.OverlapSphere(transform.position, range, mask);
        foreach(var t in targets)
        {
            if(t.gameObject.GetComponent<Health>() != null)
            {
                target = t.gameObject;
                break;
            }
        }
    }

    void SeekTarget()
    {
        if(target == null)
            return;
        var dir = target.transform.position - transform.position;
        dir.Normalize();
        direction = dir;
    }

    void Move()
    {
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    void DestroySelf()
    {
        if(gameObject != null)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target)
        {
            var health = target.GetComponent<Health>();
            if(health != null)
            {
                health.TakeDamage(Mathf.RoundToInt(damage));
            }
            Destroy(gameObject);
        }
        Debug.Log("Hit " + other.gameObject.name);
    }
}