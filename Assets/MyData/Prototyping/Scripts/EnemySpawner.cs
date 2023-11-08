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

    private float currentTime = 0f;
    private float spawnInterval = 15f;

    private int currentWaveIndex = 0;
    private int currentWaveEnemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnWave();
        enemyCountText.text = $"Remaining Enemies: {currentWaveEnemyCount:D2}";
    }

    private void OnEnable()
    {
        EnemyController.OnAnyEnemyKilled += OnAnyEnemyKilled;
    }

    private void OnAnyEnemyKilled()
    {
        currentWaveEnemyCount--;

        // Fast forward wave
        if (currentWaveEnemyCount <= 0 )
        {
            SpawnWave();
        }
    }

    private void OnDisable()
    {
        EnemyController.OnAnyEnemyKilled -= OnAnyEnemyKilled;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > spawnInterval && currentWaveIndex < enemyWavesList.Count)
        {
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        currentTime = 0f;
        spawnInterval = enemyWavesList[currentWaveIndex].nextWaveTime;
        AnimateWaveText();
        TempWaveSpawn();
        currentWaveIndex++;
    }

    private void AnimateWaveText()
    {
        waveInfoText.text = $"Wave #{currentWaveIndex + 1}";
        waveInfoText.transform.LeanScale(Vector3.one, 1f).setLoopPingPong(1).setEaseOutBounce().setDelay(1f);
    }

    #region Temp Methods

    private void TempWaveSpawn()
    {
        var spawnPosTop = player.transform.position + (Vector3.up * 15f);
        var spawnPosBottom = player.transform.position + (Vector3.down * 15f);
        var spawnPosLeft= player.transform.position + (Vector3.left * 15f);
        var spawnPosRight = player.transform.position + (Vector3.right * 15f);

        spawnPosTop.z = spawnPosBottom.z = spawnPosLeft.z = spawnPosRight.z = 0f;
        var waveObj = new GameObject($"Wave {currentWaveIndex + 1}");
        waveObj.transform.SetParent(transform);
        int spawnCount = 0;
        foreach (var enemyInfo in enemyWavesList[currentWaveIndex].enemiesList)
        {
            for (int i = 0; i < enemyInfo.spawnCount; i++, spawnCount += 4)
            {
                var enemy = enemyInfo.enemy;

                spawnPosTop.x = Random.Range(spawnPosTop.x - 10, spawnPosTop.x + 10);
                var enemy1 = Instantiate(enemy, spawnPosTop, Quaternion.identity, waveObj.transform);

                spawnPosBottom.x = Random.Range(spawnPosBottom.x - 10, spawnPosBottom.x + 10);
                var enemy2 = Instantiate(enemy, spawnPosBottom, Quaternion.identity, waveObj.transform);

                spawnPosLeft.y = Random.Range(spawnPosLeft.y - 10, spawnPosLeft.y + 10);
                var enemy3 =Instantiate(enemy, spawnPosLeft, Quaternion.identity, waveObj.transform);

                spawnPosRight.y = Random.Range(spawnPosRight.y - 10, spawnPosRight.y + 10);
                var enemy4 = Instantiate(enemy, spawnPosRight, Quaternion.identity, waveObj.transform);

                enemy1.GetComponent<HealthManager>().SetMaxHealth(enemyInfo.healthMultiplier);
                enemy2.GetComponent<HealthManager>().SetMaxHealth(enemyInfo.healthMultiplier);
                enemy3.GetComponent<HealthManager>().SetMaxHealth(enemyInfo.healthMultiplier);
                enemy4.GetComponent<HealthManager>().SetMaxHealth(enemyInfo.healthMultiplier);
            }
        }

        currentWaveEnemyCount = spawnCount;
    }

    #endregion
}
