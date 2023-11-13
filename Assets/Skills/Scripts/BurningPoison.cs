using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningPoison : SkillActivator
{
    [SerializeField] float damage;
    [SerializeField] float radius;

    public override int MaxLevel => 1;
    public override void Activate()
    {
        vfx.localScale = radius * 2 * Vector3.one;
        base.Activate();
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void UseSkill()
    {
        var enemies = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
        foreach (var enemy in enemies)
        {
            var enemyComponent = enemy.GetComponent<IEnemy>();
            enemyComponent.Damage(skillsAugmentor.CalculateModifiedDamage(damage, enemyComponent.IsBoss));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
