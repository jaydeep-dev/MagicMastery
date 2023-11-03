using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // This will change to EnemyWave class 
    [SerializeField] private List<GameObject> enemiesList;
    [SerializeField] private float spawnInterval = 15f;

    private float currentTime = 0f;

    #region Temp Code
    [Header("Temp Code")]
    [SerializeField] private Camera mainCam;
    [SerializeField] private int spawnCount;
    [SerializeField] private bool useSpawner;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SpawnWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (useSpawner)
            currentTime += Time.deltaTime;

        if (currentTime > spawnInterval)
        {
            currentTime = 0f;
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        TempWaveSpawn();
    }

    #region Temp Methods

    private void TempWaveSpawn()
    {
        var spawnPosTop = mainCam.transform.position + (Vector3.up * 15f);
        var spawnPosBottom = mainCam.transform.position + (Vector3.down * 15f);
        var spawnPosLeft= mainCam.transform.position + (Vector3.left * 15f);
        var spawnPosRight = mainCam.transform.position + (Vector3.right * 15f);


        spawnPosTop.z = spawnPosBottom.z = spawnPosLeft.z = spawnPosRight.z = 0f;

        for (int i = 0; i < spawnCount; i++)
        {
            var enemy = enemiesList[Random.Range(0, enemiesList.Count)];

            spawnPosTop.x = Random.Range(spawnPosTop.x - 10, spawnPosTop.x + 10);
            Instantiate(enemy, spawnPosTop, Quaternion.identity);

            enemy = enemiesList[Random.Range(0, enemiesList.Count)];
            spawnPosBottom.x = Random.Range(spawnPosBottom.x - 10, spawnPosBottom.x + 10);
            Instantiate(enemy, spawnPosBottom, Quaternion.identity);

            enemy = enemiesList[Random.Range(0, enemiesList.Count)];
            spawnPosLeft.y = Random.Range(spawnPosLeft.y - 10, spawnPosLeft.y + 10);
            Instantiate(enemy, spawnPosLeft, Quaternion.identity);

            enemy = enemiesList[Random.Range(0, enemiesList.Count)];
            spawnPosRight.y = Random.Range(spawnPosRight.y - 10, spawnPosRight.y + 10);
            Instantiate(enemy, spawnPosRight, Quaternion.identity);
        }
    }

    #endregion
}
