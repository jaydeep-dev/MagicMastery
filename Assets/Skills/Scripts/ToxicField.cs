using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicField : SkillActivator
{
    [SerializeField] float baseRadius;
    [SerializeField] float baseDamage;
    [SerializeField] float increasedRadius;
    [SerializeField] float increasedDamage;

    public override void Activate()
    {
        base.Activate();
        vfx.localScale = baseRadius * 2 * Vector3.one;
    }

    public override void LevelUp()
    {
        base.LevelUp();
        if(CurrentLevel == 3)
        {
            vfx.localScale =  increasedRadius * 2 * Vector3.one;
        }
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void UseSkill()
    {
        float radius = CurrentLevel == 3 ? increasedRadius : baseRadius;
        float damage = CurrentLevel == 1 ? baseDamage : increasedDamage;
        var enemies = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
        foreach(var enemy in enemies)
        {
            var enemyComponent = enemy.GetComponent<IEnemy>();
            enemyComponent.Damage(skillsAugmentor.CalculateModifiedDamage(damage, enemyComponent.IsBoss));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CurrentLevel == 3 ? increasedRadius : baseRadius);
    }
}
