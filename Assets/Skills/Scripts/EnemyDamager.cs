using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damageMultiplier = 1f;

    public void DamageEnemy(IEnemy enemy, float damage)
    {
        enemy.Damage(damage * damageMultiplier);
    }    
}
