using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHelath;

    public event System.Action OnDie;

    [field: SerializeField] public float Health { get; private set; }

    private void Start()
    {
        Health = maxHelath;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(transform.name + " Is Taking Damage");
        Health -= damage;

        if (Health <= 0)
            Die();

    }

    private void Die()
    {
        Debug.Log(transform.name + " is dead");
        Destroy(gameObject);
        OnDie?.Invoke();
    }
}
