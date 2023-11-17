using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth;

    public event System.Action OnDie;
    public event System.Action OnDamageTaken;

    [field: SerializeField] public float Health { get; private set; }
    public float MaxHealth => maxHealth;

    private void Start()
    {
        Health = maxHealth;
    }

    public void SetMaxHealth(float healthMultiplier) => maxHealth *= healthMultiplier;

    public void TakeDamage(float damage)
    {
        //Debug.Log(transform.name + " Is Taking Damage");
        Health -= damage;
        OnDamageTaken?.Invoke();

        if (Health <= 0)
            Die();

    }

    private void Die()
    {
        Debug.Log(transform.name + " is dead");
        OnDie?.Invoke();
    }
}
