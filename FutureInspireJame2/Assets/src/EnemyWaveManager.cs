using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] EnemySpawner m_spawner = default;
    [SerializeField] Enemy m_spawnRef = default;
    [SerializeField] PlayerHomeBase m_playerBase;
    float m_waveTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        m_spawner?.SpawnEnemies(m_spawnRef, 5, 0.5f, InitEnenmy);
    }
    void StartWave() 
    { 
    
    }
    void InitEnenmy(Enemy spawned) 
    {
        spawned.Init(m_playerBase.transform);
    }
}
