using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemySpawnItem
{
    [SerializeField] int m_numToSpawn;
    [SerializeField] Enemy m_spawnRef;
    public int NumToSpawn => m_numToSpawn;
    public Enemy SpawnRef => m_spawnRef;
}
//Contains all enemies to spawn in a wave
[CreateAssetMenu(fileName = "Wave_", menuName = "Jams/EnemySpawning/Create a new enemy wave detail", order = 1)]
public class EnemyWaveDetail : ScriptableObject 
{
    // time to wait before next wave, start counting down after we spawned all enemies in this wave 
    [SerializeField] float m_secondsBeforeNextWave = default;
    // A group of enemies spawn from the same spawner location
    [SerializeField] List<EnemySpawnItem> m_groupsToSpawn = default;
    public float SecondsBeforeNextWave => m_secondsBeforeNextWave;
    public List<EnemySpawnItem> GroupsToSpawn => m_groupsToSpawn;
}
