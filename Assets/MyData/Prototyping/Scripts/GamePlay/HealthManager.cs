using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxDefence;
    private float defence;

    public event System.Action OnDie;
    public event System.Action OnDamageTaken;

    [field: SerializeField] public float Health { get; private set; }
    public float MaxHealth => maxHealth;

    private void Start()
    {
        Health = maxHealth;
        defence = maxDefence;
    }

    public void SetDefenceMultiplier(float multiplier) => maxDefence *= multiplier;

    public void SetMaxHealth(float healthMultiplier) => maxHealth *= healthMultiplier;

    public void SetCurrentHealth(float multiplier)
    {
        Debug.Log("Health Before Modification: " + Health);
        Health += maxHealth * multiplier; // Add health to player
        Health = Mathf.Clamp(Health, 0, MaxHealth); // restrict health to prevent overshoot.
        Debug.Log("Health After Modification: " + Health);
    }

    public void TakeDamage(float damage)
    {
        //Debug.Log(transform.name + " Is Taking Damage");
        defence -= damage;
        if (defence <= 0)
        {
            // dmg after defence is broken
            float dmg = Mathf.Abs(defence);
            Health -= dmg;
            defence = maxDefence;
            OnDamageTaken?.Invoke();
        }

        if (Health <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log(transform.name + " is dead");
        OnDie?.Invoke();
    }
}
