using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I know it's exactly the same code as FireMissileProjectile, but we might have changes later on so kept a separate script.
//It's quick and dirty. I know.
public class WindGust : SkillActivator
{
    [SerializeField] SimpleProjectile projectilePrefab;
    [SerializeField] float baseDamage;
    [SerializeField] float increasedDamage;
    [SerializeField] float enemyDetectRange;
    [SerializeField] AudioSource projectileLaunchAudio;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void UseSkill()
    {
        int numberOfProjectiles = CurrentLevel == 1 ? 1 : 2;
        float damage = CurrentLevel == 3 ? increasedDamage : baseDamage;

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
