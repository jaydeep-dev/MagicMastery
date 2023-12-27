using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // This will change to EnemyWave class 
    [SerializeField] private PlayerMovement player;
    [SerializeField] private TextMeshProUGUI waveInfoText;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private List<EnemyWaveSO> enemyWavesList;

    [Header("Boss Settings")]
    [SerializeField] private GameObject bossPrefab;

    private float currentTime = 0f;
    private float spawnInterval = 15f;

    private int currentWaveIndex = 0;
    private int remainimgEnemyCount = 0;

    public static event System.Action<float> OnWaveSpawned;

    // Start is called before the first frame update
    void Start()
    {
        SpawnWave();
    }

    private void OnEnable()
    {
        EnemyController.OnAnyEnemyKilled += OnAnyEnemyKilled;
        ProgressbarUIController.OnTimeOver += SpawnBossEnemy;
    }

    private void OnAnyEnemyKilled()
    {
        remainimgEnemyCount--;
        UpdateRemainingCount();

        // Fast forward wave
        if (remainimgEnemyCount <= 0)
        {
            SpawnWave();
        }
    }

    private void UpdateRemainingCount()
    {
        enemyCountText.text = $"Remaining Enemies: {remainimgEnemyCount:D2}";
    }

    public void SpawnBossEnemy()
    {
        KillRemainingEnemies();
        Instantiate(bossPrefab, player.transform.position + (Vector3.up * 10f), Quaternion.identity);
    }

    private void KillRemainingEnemies()
    {
        gameObject.SetActive(false);
        foreach (var enemy in FindObjectsOfType<EnemyController>())
        {
            enemy.GetComponent<IEnemy>().Damage(100 * 100);
        }
    }

    private void OnDisable()
    {
        EnemyController.OnAnyEnemyKilled -= OnAnyEnemyKilled;
        ProgressbarUIController.OnTimeOver -= SpawnBossEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > spawnInterval && currentWaveIndex < enemyWavesList.Count)
        {
            if(GameManager.IsGodMode && currentWaveIndex + 1 >= enemyWavesList.Count)
            {
                currentWaveIndex = enemyWavesList.Count - 1;
            }
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        AnimateWaveText();
        var remainingTime = spawnInterval - currentTime;
        currentTime = 0f;
        spawnInterval = GetEnemyWave().nextWaveTime;
        SpawnEnemies();

        UpdateRemainingCount();

        if (currentWaveIndex != 0)
            OnWaveSpawned?.Invoke(remainingTime);
        currentWaveIndex++;
    }

    private void AnimateWaveText()
    {
        waveInfoText.text = $"Wave #{currentWaveIndex + 1}";
        waveInfoText.transform.LeanScale(Vector3.one, 1f).setLoopPingPong(1).setEaseOutBounce();
    }

    private void SpawnEnemies()
    {
        var spawnPosTop = player.transform.position + (Vector3.up * 15f);
        var spawnPosBottom = player.transform.position + (Vector3.down * 15f);
        var spawnPosLeft= player.transform.position + (Vector3.left * 15f);
        var spawnPosRight = player.transform.position + (Vector3.right * 15f);

        spawnPosTop.z = spawnPosBottom.z = spawnPosLeft.z = spawnPosRight.z = 0f;

        spawnPosTop.x = spawnPosBottom.x = -20f;
        spawnPosLeft.y = spawnPosRight.y = -10f;

        var waveObj = new GameObject($"Wave {currentWaveIndex + 1}");
        waveObj.transform.SetParent(transform);
        int spawnCount = 0;
        var enemyWave = GetEnemyWave();
        foreach (var enemyInfo in enemyWave.enemiesList)
        {
            for (int i = 0; i < enemyInfo.spawnCount; i++, spawnCount += 4)
            {
                var enemy = enemyInfo.enemy;

                spawnPosTop.x += 2;
                var enemy1 = Instantiate(enemy, spawnPosTop, Quaternion.identity, waveObj.transform);

                spawnPosBottom.x += 2;
                var enemy2 = Instantiate(enemy, spawnPosBottom, Quaternion.identity, waveObj.transform);

                spawnPosLeft.y += 1;
                var enemy3 =Instantiate(enemy, spawnPosLeft, Quaternion.identity, waveObj.transform);

                spawnPosRight.y += 1;
                var enemy4 = Instantiate(enemy, spawnPosRight, Quaternion.identity, waveObj.transform);

                enemy1.GetComponent<HealthManager>().SetMaxHealth(enemyInfo.healthMultiplier);
                enemy2.GetComponent<HealthManager>().SetMaxHealth(enemyInfo.healthMultiplier);
                enemy3.GetComponent<HealthManager>().SetMaxHealth(enemyInfo.healthMultiplier);
                enemy4.GetComponent<HealthManager>().SetMaxHealth(enemyInfo.healthMultiplier);
            }
        }

        remainimgEnemyCount += spawnCount;
    }

    EnemyWaveSO GetEnemyWave()
    {
        if(currentWaveIndex < enemyWavesList.Count)
        {
            return enemyWavesList[currentWaveIndex];
        }

        return enemyWavesList[^1];
    }
}
