using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float maxDistance;
    [SerializeField] float speed;
    [SerializeField] bool rotateSpriteTowardsTarget;
    [SerializeField] Vector2 projectileDirectionInSprite;
    bool enemyHit;
    float damage;
    Vector2 startPoint;
    bool launched;
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public void Launch(Vector2 start, Vector2 direction)
    {
        spriteRenderer.enabled = true;
        if(rotateSpriteTowardsTarget)
        {
            rb.rotation = Vector2.SignedAngle(projectileDirectionInSprite, direction);
        }        
        rb.position = start;
        rb.velocity = speed * direction;
        startPoint = start;
        launched = true;
    }

    private void FixedUpdate()
    {
        if(!launched)
        {
            return;
        }

        if((rb.position - startPoint).sqrMagnitude >= maxDistance * maxDistance)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(enemyHit || !collision.gameObject.TryGetComponent<IEnemy>(out var enemy))
        {
            return;
        }

        enemy.Damage(damage);
        enemyHit = true;
        Destroy(gameObject);
    }
}
