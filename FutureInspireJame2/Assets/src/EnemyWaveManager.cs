using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public delegate void OnWaveStart(int waveNumber);
public delegate void OnAllWaveCleared();
public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] List<EnemyWaveDetail> m_waves = default;
    [SerializeField] List<EnemySpawner> m_spawners = default;
    [SerializeField] PlayerHomeBase m_playerBase;
    [SerializeField] PlayableDirector m_countdownDirector = default;
    [SerializeField] AudioClip m_finalWave = default;
    [SerializeField] AudioSource m_gameplayBgm = default;

    Coroutine m_spawnWave;
    public event OnAllWaveCleared OnAllCleared;
    public event OnWaveStart OnStart;
    // current wave number
    int m_currentWave = 0;
    int m_numEnemies = 0;
    bool m_waitingForNextWave = false;
    public int CurrentWave => m_currentWave;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnStartGame());
    }
    IEnumerator OnStartGame() 
    {
        yield return new WaitForSeconds(0.5f);
        // stop bgm until we start next wave
        m_countdownDirector.Play();
        yield return new WaitForSeconds(3f);
        StartWave();
        yield return new WaitForSeconds(0.8f);
        m_gameplayBgm?.Play();
    }
    public void StartWave() 
    {
        if (m_spawnWave != null) return;
        m_spawnWave = StartCoroutine(Spawn_Internal());
    }
    public void PauseWave() 
    {
        if (m_spawnWave == null) return;
        StopCoroutine(m_spawnWave);
        m_waitingForNextWave = false;
        m_spawnWave = null;
    }
    IEnumerator Spawn_Internal() 
    {
        while (m_currentWave < m_waves.Count) 
        {
            EnemyWaveDetail wave = m_waves[m_currentWave];
            // Choose random spawners from list of spawner
            List<EnemySpawner> spawners = SamplingUtil.SampleFromList(
                m_spawners, wave.GroupsToSpawn.Count, uniqueResults: true);
            int i = 0;
            OnStart?.Invoke(m_currentWave + 1);
            // spawn reward
            m_playerBase.SpawnRewardProjectiles();
            // wait for wave countdown
            yield return new WaitForSeconds(3f);
            foreach (var group in wave.GroupsToSpawn)
            {
                // spawn the group
                spawners[i].SpawnEnemies(group.SpawnRef, group.NumToSpawn, 0.1f, InitEnenmy);
                // increment enemy count
                m_numEnemies += group.NumToSpawn;
                i++;
                yield return new WaitForSeconds(0.9f);
            }
            // after spawning, wait for a set duration
            m_waitingForNextWave = true;
            yield return new WaitForSeconds(wave.SecondsBeforeNextWave);
            m_waitingForNextWave = false;
            // Increment to spawn next wave
            m_currentWave++;
        }
        m_spawnWave = null;
    }
    void InitEnenmy(Enemy spawned) 
    {
        if(spawned is BossYeetus) 
        {
            m_gameplayBgm.Stop();
            m_gameplayBgm.clip = m_finalWave;
            m_gameplayBgm.Play();
        }

        spawned.Init(m_playerBase.transform);
        spawned.OnDefeat += OnEnemyDefeated;
    }
    void OnEnemyDefeated(Enemy defeated) 
    {
        defeated.OnDefeat -= OnEnemyDefeated;
        m_numEnemies--;
        // if we cleared the wave early
        if(m_numEnemies == 0 && m_currentWave < m_waves.Count && m_waitingForNextWave) 
        {
            m_waitingForNextWave = false;
            // spawn reward
            m_playerBase.SpawnRewardProjectiles();
            PauseWave();
            m_currentWave++;
            StartWave();
        }
        else if (m_numEnemies == 0 && m_currentWave >= m_waves.Count) 
        {
            OnAllCleared?.Invoke();
        }
    }
}
