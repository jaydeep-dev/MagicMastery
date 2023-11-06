using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFieldInstance : MonoBehaviour
{
    [SerializeField] CircleCollider2D circleCollider;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float timeBetweenDamage;
    [SerializeField] float destroyTime;
    float damage;
    float timeLeft;
    public void SetRadius(float radius)
    {
        transform.localScale = radius * 2 * Vector3.one;
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            timeLeft = timeBetweenDamage;
            var enemies = new List<Collider2D>();
            circleCollider.OverlapCollider(new ContactFilter2D()
            {
                layerMask = enemyLayer,
                useLayerMask = true
            }, enemies);
            foreach(var enemy in enemies)
            {
                enemy.GetComponent<IEnemy>().Damage(damage);
            }
        }
    }
}
