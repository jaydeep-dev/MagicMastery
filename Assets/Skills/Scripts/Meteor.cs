using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] Transform vfxMeteor;
    [SerializeField] Transform vfxExplosion;
    [SerializeField] float projectileTravelTime;
    [SerializeField] Vector2 directionInSprite;
    [SerializeField] float launchToTargetDistance;
    [SerializeField] float vfxExplosionDuration;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] AudioSource meteorImpactSound;
    bool isActive = false;
    Vector2 travelDirection;
    Vector2 launchPoint;
    Vector2 targetPoint;
    float timeLeft;
    float damage;
    float damageRadius;
    SkillsAugmentor skillsAugmentor;
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public void InjectAugmentor(SkillsAugmentor augmentor)
    {
        skillsAugmentor = augmentor;
    }
    public void SetDamageRadius(float damageRadius)
    {
        vfxExplosion.localScale = damageRadius * 2 * Vector3.one;
        vfxMeteor.localScale = damageRadius * 2 * Vector3.one;
        this.damageRadius = damageRadius;
    }
    public void SetTravelDirection(Vector2 travelDirection)
    {
        this.travelDirection = travelDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive)
        {
            return;
        }

        timeLeft -= Time.deltaTime;
        float t = 1 - (timeLeft / projectileTravelTime);
        transform.position = Vector2.Lerp(launchPoint, targetPoint, t);
        if(t >= 1)
        {
            isActive = false;
            transform.position = targetPoint;
            vfxExplosion.gameObject.SetActive(true);
            Destroy(gameObject, vfxExplosionDuration);
            vfxMeteor.gameObject.SetActive(false);
            meteorImpactSound.Play();
            var colliders = Physics2D.OverlapCircleAll(targetPoint, damageRadius, enemyLayer);
            foreach(var collider in colliders)
            {
                var enemy = collider.gameObject.GetComponent<IEnemy>();
                enemy.Damage(skillsAugmentor.CalculateModifiedDamage(damage, enemy.IsBoss));
            }
        }
    }

    public void Launch(Vector2 targetPoint)
    {
        this.targetPoint = targetPoint;
        launchPoint = targetPoint - travelDirection * launchToTargetDistance;
        transform.position = launchPoint;
        vfxMeteor.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(directionInSprite, travelDirection));
        vfxMeteor.gameObject.SetActive(true);
        timeLeft = projectileTravelTime;
        isActive = true;
    }
}
