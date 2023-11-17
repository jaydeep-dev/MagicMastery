using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellSummon : SkillActivator
{
    [SerializeField] float damage;
    [SerializeField] float damageRadius;
    [SerializeField] Meteor meteorPrefab;
    [SerializeField] int meteorCount;
    [SerializeField] float attackRadius;

    protected override void Update()
    {
        base.Update();
    }

    protected override void UseSkill()
    {        
        var colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyLayer);
        foreach (var collider in colliders)
        {
            var enemy = collider.GetComponent<IEnemy>();
            if(enemy.IsBoss)
            {
                for(int i=0;i<meteorCount;i++)
                {
                    FireMeteor(collider.transform.position);
                }

                return;
            }
        }

        for (int i = 0; i < meteorCount; i++)
        {            
            var target = new Vector2(transform.position.x, transform.position.y) + attackRadius * Random.insideUnitCircle;
            FireMeteor(target);
        }
    }

    void FireMeteor(Vector2 target)
    {
        var meteor = Instantiate(meteorPrefab);
        meteor.SetDamage(damage);
        meteor.InjectAugmentor(skillsAugmentor);
        meteor.SetDamageRadius(damageRadius);
        meteor.SetTravelDirection(Quaternion.Euler(0, 0, Random.Range(-60, 60)) * Vector3.down);
        meteor.Launch(target);
    }
}
