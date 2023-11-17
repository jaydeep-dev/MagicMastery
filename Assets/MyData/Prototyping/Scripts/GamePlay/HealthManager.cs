using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float defence;

    public event System.Action OnDie;
    public event System.Action OnDamageTaken;

    [field: SerializeField] public float Health { get; private set; }
    public float MaxHealth => maxHealth;
    private float defenceInternal;

    private void Start()
    {
        Health = maxHealth;
        defenceInternal = defence;
    }

    public void SetDefenceMultiplier(float multiplier) => defence *= multiplier;

    public void SetCurrentHealth(float multiplier) => Health = maxHealth * multiplier;

    public void SetMaxHealth(float healthMultiplier) => maxHealth *= healthMultiplier;

    public void TakeDamage(float damage)
    {
        //Debug.Log(transform.name + " Is Taking Damage");
        defence -= damage;
        if (defence < 0)
        {
            // dmg after defence is broken
            float dmg = Mathf.Abs(defence);
            Health -= dmg;
            defence = defenceInternal;
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
