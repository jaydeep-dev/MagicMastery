using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionalArrowProjectile : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] Vector2 projectileDirectionInSprite;
    [SerializeField] float duration;
    [SerializeField] AudioSource arrowHitSound;
    [SerializeField] LayerMask dimensionalColliderLayer;
    float damage;
    bool launched;
    SkillsAugmentor skillsAugmentor;
    float timeLeft;
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public void InjectAugmentor(SkillsAugmentor augmentor)
    {
        skillsAugmentor = augmentor;
    }
    public void Launch(Vector2 start, Vector2 direction)
    {
        spriteRenderer.enabled = true;
        rb.rotation = Vector2.SignedAngle(projectileDirectionInSprite, direction);
        rb.position = start;
        rb.velocity = speed * direction;
        launched = true;
        timeLeft = duration;
    }

    private void Update()
    {
        if (!launched)
        {
            return;
        }

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1<<collision.gameObject.layer) & dimensionalColliderLayer) != 0)
        {
            //claculate normal
            var inDirection = rb.velocity.normalized;
            var pointOutsideCollider = rb.position + 5 * -inDirection;
            var normal = (pointOutsideCollider - collision.ClosestPoint(pointOutsideCollider)).normalized;
            var outDirection = Vector2.Reflect(inDirection, normal);
            rb.velocity = speed * outDirection;
            rb.rotation = Vector2.SignedAngle(projectileDirectionInSprite, outDirection);
            arrowHitSound.Play();
            return;
        }

        if (!collision.gameObject.TryGetComponent<IEnemy>(out var enemy))
        {
            return;
        }

        enemy.Damage(skillsAugmentor.CalculateModifiedDamage(damage, enemy.IsBoss));
    }
}
