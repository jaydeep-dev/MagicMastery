using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Wave", menuName = "Enemy/Enemy Wave")]
public class EnemyWaveSO : ScriptableObject
{
    public List<EnemyWaveInfo> enemiesList = new List<EnemyWaveInfo>();
    public float nextWaveTime;
}

[System.Serializable]
public struct EnemyWaveInfo
{
    public EnemyController enemy;
    public int spawnCount;
    public float healthMultiplier;
}
