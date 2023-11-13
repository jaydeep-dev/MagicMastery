using System.Collections.Generic;
using UnityEngine;

public class LighteningBolt : SkillActivator
{
    [SerializeField] float enemyDetectRange;
    [SerializeField] List<LineRenderer> lineRenderersBolt1;
    [SerializeField] List<LineRenderer> lineRenderersBolt2;
    [SerializeField] float minDistanceBetweenPoints;
    [SerializeField] float maxDistanceBetweenPoints;
    [SerializeField] Vector2 maxOffset;
    [Range(0, 0.5f)]
    [SerializeField] float effectVisibilityTime;
    [Range(0, 100)]
    [SerializeField] float baseDamage;
    [SerializeField] float increasedDamage;
    [SerializeField] AudioSource lighteningAudio;
    // Start is called before the first frame update

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void UseSkill()
    {
        float damageToDeal = CurrentLevel == 3 ? increasedDamage : baseDamage;
        int numberOfBolts = CurrentLevel == 1 ? 1 : 2;
        for (int i = 0; i < numberOfBolts; i++)
        {
            List<Vector3> path = new() { transform.position };
            Vector3 detectionCentre = transform.position;
            for (int j = 0; j < 3; j++)
            {
                var enemiesFound = Physics2D.OverlapCircleAll(detectionCentre, enemyDetectRange, enemyLayer);
                if (enemiesFound.Length == 0)
                {
                    return;
                }
                var enemy = enemiesFound[Random.Range(0, enemiesFound.Length)];
                path.Add(enemy.transform.position);
                detectionCentre = enemy.transform.position;
                var enemyComponent = enemy.GetComponent<IEnemy>();
                enemyComponent.Damage(skillsAugmentor.CalculateModifiedDamage(damageToDeal, enemyComponent.IsBoss));
            }

            DrawLightening(path, i + 1);
            lighteningAudio.Play();
            if (i == 0)
            {
                Invoke(nameof(DisableLighteningBolt1), effectVisibilityTime);
            }
            else
            {
                Invoke(nameof(DisableLighteningBolt2), effectVisibilityTime);
            }

        }
    }

    void DisableLighteningBolt1()
    {
        SetLighteningVisibility(false, 1);
    }
    void DisableLighteningBolt2()
    {
        SetLighteningVisibility(false, 2);
    }
    void SetLighteningVisibility(bool isVisible, int boltNumber)
    {
        var lineRenderers = boltNumber == 1 ? lineRenderersBolt1 : lineRenderersBolt2;
        foreach (var lineRenderer in lineRenderers)
        {
            lineRenderer.enabled = isVisible;
        }
    }

    void DrawLightening(List<Vector3> path, int boltNumber)
    {
        var lineRenderers = boltNumber == 1 ? lineRenderersBolt1 : lineRenderersBolt2;
        foreach (var lineRenderer in lineRenderers)
        {
            List<Vector3> points = new();
            points.Add(path[0]);
            for (int i = 1; i < path.Count; i++)
            {
                Vector3 diff = path[i] - path[i - 1];
                Vector3 direction = diff.normalized;
                float angle = Vector3.SignedAngle(Vector3.right, direction, Vector3.forward);
                float totalDistance = diff.magnitude;
                float distanceTravelled = 0;
                while (true)
                {
                    float step = Random.Range(minDistanceBetweenPoints, maxDistanceBetweenPoints);
                    distanceTravelled += step;
                    if (distanceTravelled >= totalDistance)
                    {
                        points.Add(path[i]);
                        break;
                    }

                    float offsetX = Random.Range(-maxOffset.x, maxOffset.x);
                    float offsetY = Random.Range(-maxOffset.y, maxOffset.y);
                    var newPoint = path[i - 1] + Quaternion.Euler(0, 0, angle) * new Vector3(distanceTravelled + offsetX, offsetY);
                    points.Add(newPoint);
                }
            }

            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }

        SetLighteningVisibility(true, boltNumber);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDetectRange);
    }
}
