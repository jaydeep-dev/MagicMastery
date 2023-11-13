using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShot : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float animationAttackTimeNormalized;
    [SerializeField] AudioSource iceShotSound;
    SkillsAugmentor skillsAugmentor;
    public void UseIceShot(Vector2 direction, float damage)
    {
        StartCoroutine(IceShotRoutine(direction, damage));
    }
    public void InjectSkillsAugmentor(SkillsAugmentor augmentor)
    {
        skillsAugmentor = augmentor;
    }

    IEnumerator IceShotRoutine(Vector2 direction, float damage)
    {
        transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, direction));
        animator.SetTrigger("IceShot");
        spriteRenderer.enabled = true;
        iceShotSound.Play();
        float normalizedTime = 0;
        bool attacked = false;
        while(normalizedTime <= 1)
        {
            var animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if(animatorStateInfo.IsName("IceShot"))
            {
                normalizedTime = animatorStateInfo.normalizedTime;
            }
            
            if(!attacked && normalizedTime >= animationAttackTimeNormalized)
            {
                var results = new List<Collider2D>();
                boxCollider.OverlapCollider(new ContactFilter2D()
                {
                    layerMask = enemyLayer,
                    useLayerMask = true
                }, results);

                foreach(var result in results)
                {
                    var enemy = result.GetComponent<IEnemy>();
                    enemy.Damage(skillsAugmentor.CalculateModifiedDamage(damage, enemy.IsBoss));
                }

                attacked = true;
            }

            yield return null;
        }

        spriteRenderer.enabled = false;
    }
}
