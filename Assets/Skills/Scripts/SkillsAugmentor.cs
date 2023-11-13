using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsAugmentor : MonoBehaviour
{
    public float damageMultiplier = 1;
    public float cooldownMultiplier = 1;
    public float bossEnemyDamageMultiplier = 1;
    public float CalculateModifiedDamage(float damage, bool isBossEnemy)
    {
        if(isBossEnemy)
        {
            return damage * (damageMultiplier + bossEnemyDamageMultiplier - 1);
        }

        return damage * damageMultiplier;
    }
    public float CalculateModifiedCooldown(float cooldown)
    {
        return cooldown * cooldownMultiplier;
    }
}