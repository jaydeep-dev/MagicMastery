using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSummon : SkillActivator
{
    [SerializeField] float baseDamage;
    [SerializeField] float increasedDamage;
    [SerializeField] float baseDamageRadius;
    [SerializeField] float increasedDamageRadius;
    [SerializeField] Meteor meteorPrefab;
    [SerializeField] int meteorCount;
    [SerializeField] float attackRadius;

    protected override void Update()
    {
        base.Update();
    }

    protected override void UseSkill()
    {
        float damage = CurrentLevel == 3 ? increasedDamage : baseDamage;
        float damageRadius = CurrentLevel == 1 ? baseDamageRadius : increasedDamageRadius;
        for(int i=0;i<meteorCount;i++)
        {
            var target = new Vector2(transform.position.x, transform.position.y) + attackRadius * Random.insideUnitCircle;
            var meteor = Instantiate(meteorPrefab);
            meteor.SetDamage(damage);
            meteor.InjectAugmentor(skillsAugmentor);
            meteor.SetDamageRadius(damageRadius);
            meteor.SetTravelDirection(Quaternion.Euler(0, 0, Random.Range(-60, 60)) * Vector3.down);
            meteor.Launch(target);
        }        
    }
    
}
