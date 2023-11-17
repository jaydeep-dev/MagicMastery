using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceField : SkillActivator
{
    [SerializeField] float baseRadius;
    [SerializeField] float increasedRadius;
    [SerializeField] float damage;
    [SerializeField] float slowSpeedMultiplier;
    List<Collider2D> slowedEnemies = new();

    public override void Activate()
    {
        base.Activate();
        vfx.localScale = baseRadius * 2 * Vector3.one;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        if (CurrentLevel == 3)
        {
            vfx.localScale = increasedRadius * 2 * Vector3.one;
        }
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void UseSkill()
    {
        float radius = CurrentLevel == 3 ? increasedRadius : baseRadius;
        var enemies = new List<Collider2D>(Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer));
        if(CurrentLevel == 1)
        {
            foreach(var enemy in enemies)
            {
                var enemyComponent = enemy.GetComponent<IEnemy>();
                enemyComponent.Damage(skillsAugmentor.CalculateModifiedDamage(damage, enemyComponent.IsBoss));
            }

            return;
        }

        //crude implementation. Can be optimized later if required.
       
        //damage and slow enemies within radius
        foreach (var enemy in enemies)
        {
            var enemyComponent = enemy.GetComponent<IEnemy>();
            if(CurrentLevel != 1)
            {
                enemyComponent.Damage(skillsAugmentor.CalculateModifiedDamage(damage, enemyComponent.IsBoss));
            }            

            if(!slowedEnemies.Contains(enemy))
            {
                enemyComponent.ChangeSpeed(slowSpeedMultiplier);
                slowedEnemies.Add(enemy);
            }
        }

        slowedEnemies.RemoveAll(x => x == null);

        //set the speed back to normal for enemies outside radius
        for(int i= 0; i < slowedEnemies.Count; i++)
        {
            if (!enemies.Contains(slowedEnemies[i]))
            {
                slowedEnemies[0].GetComponent<IEnemy>().ChangeSpeed(1);
                slowedEnemies.RemoveAt(0);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, CurrentLevel == 3 ? increasedRadius : baseRadius);
    }
}
