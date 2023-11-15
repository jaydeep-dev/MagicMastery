using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionalArrow : SkillActivator
{
    [SerializeField] DimensionalArrowProjectile projectilePrefab;
    [SerializeField] float baseDamage;
    [SerializeField] float increasedDamage;
    

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void UseSkill()
    {
        int numberOfProjectiles = CurrentLevel == 1 ? 1 : 2;
        float damage = CurrentLevel == 3 ? increasedDamage : baseDamage;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            var direction = Random.insideUnitCircle.normalized;
            var projectile = Instantiate(projectilePrefab);
            projectile.SetDamage(damage);
            projectile.InjectAugmentor(skillsAugmentor);
            projectile.Launch(transform.position, direction);
        }
    }

    
}
