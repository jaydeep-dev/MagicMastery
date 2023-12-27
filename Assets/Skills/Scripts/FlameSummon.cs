using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSummon : SkillActivator
{
    [SerializeField] GameObject flameVFXPrefab;
    [SerializeField] float baseDamage;
    [SerializeField] float increasedDamage;
    [SerializeField] float enemyDetectRange;
    [SerializeField] float vfxDuration;
    [SerializeField] AudioSource flameSound;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void UseSkill()
    {
        float damage = CurrentLevel == 1 ? baseDamage : increasedDamage;
        var colliders = Physics2D.OverlapCircleAll(transform.position, enemyDetectRange, enemyLayer);
        if(colliders.Length == 0)
        {
            return;
        }
        foreach(var collider in colliders)
        {
            var enemy = collider.gameObject.GetComponent<IEnemy>();
            if(enemy.IsBoss)
            {
                StartCoroutine(DamageEnemy(enemy, damage));
                CreateFlameVFX(collider.transform);
                return;
            }
        }

        var target = colliders[Random.Range(0, colliders.Length)];
        var targetEnemyComponent = target.GetComponent<IEnemy>();
        StartCoroutine(DamageEnemy(targetEnemyComponent, damage));
        CreateFlameVFX(target.transform);        
        return;
    }

    void CreateFlameVFX(Transform target)
    {
        var vfx = Instantiate(flameVFXPrefab, target);
        vfx.transform.localPosition = Vector3.zero;
        vfx.SetActive(true);
        Destroy(vfx, vfxDuration);
        flameSound.Play();
    }

    IEnumerator DamageEnemy(IEnemy enemy, float damage)
    {
        yield return new WaitForSeconds(vfxDuration / 2);
        if (enemy != null)
            enemy.Damage(skillsAugmentor.CalculateModifiedDamage(damage, enemy.IsBoss));
    }
}
