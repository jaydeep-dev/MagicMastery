using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatFireBall : SkillActivator
{
    [SerializeField] SimpleProjectile projectilePrefab;
    [SerializeField] float damage;
    [SerializeField] float enemyDetectRange;
    [SerializeField] AudioSource projectileLaunchAudio;
    [SerializeField] int numberOfProjectiles;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void UseSkill()
    {
        var enemies = Physics2D.OverlapCircleAll(transform.position, enemyDetectRange, enemyLayer);
        if (enemies.Length == 0)
        {
            return;
        }

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            var target = enemies[Random.Range(0, enemies.Length)];
            var projectile = Instantiate(projectilePrefab);
            projectile.SetDamage(damage);
            projectile.InjectAugmentor(skillsAugmentor);
            projectile.Launch(transform.position, (target.transform.position - transform.position).normalized);
            projectileLaunchAudio.Play();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDetectRange);
    }
}
