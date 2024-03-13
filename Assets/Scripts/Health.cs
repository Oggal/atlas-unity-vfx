using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 100;

    public UnityEvent onDeath;
    public UnityEvent<int> onDamageTaken;

    public void LateUpdate()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        onDamageTaken.Invoke(damage);
    }

    private void Die()
    {
        onDeath.Invoke();
        Destroy(gameObject);
    }
}
