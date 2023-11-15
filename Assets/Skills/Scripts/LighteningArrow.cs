using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighteningArrow : SkillActivator
{
    [SerializeField] DimensionalArrowProjectile projectilePrefab;
    [SerializeField] float damage;
    [SerializeField] AudioSource lighteningAudio;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void UseSkill()
    {
        int numberOfProjectiles = 3;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            var direction = Random.insideUnitCircle.normalized;
            var projectile = Instantiate(projectilePrefab);
            projectile.SetDamage(damage);
            projectile.InjectAugmentor(skillsAugmentor);
            projectile.Launch(transform.position, direction);
            lighteningAudio.Play();
        }
    }
}
