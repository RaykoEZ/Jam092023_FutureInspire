using Curry.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public delegate void OnGameOver();
[RequireComponent(typeof(Collider2D))]
public class PlayerHomeBase : MonoBehaviour
{
    [SerializeField] int m_maxHp = default;
    [SerializeField] float m_projectileSpawnInterval = default;
    [SerializeField] int m_spawnLimit = default;
    [SerializeField] List<DraggableProjectile> m_rewardList = default;
    [SerializeField] ResourceBar m_hpBar = default;
    [SerializeField] PlayableDirector m_anim = default;
    [SerializeField] DraggableProjectile m_toSpawn = default;
    [SerializeField] RangedSpawner m_spawner = default;
    int m_projectilesSpawned = 0;
    int m_currentHp = 0;
    public event OnGameOver GameOver;
    private void Awake()
    {
        m_currentHp = m_maxHp;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_hpBar.SetMaxValue(m_maxHp);
        m_hpBar.SetBarValue(m_currentHp);
        StartCoroutine(SpawnProjectile());
    }

    IEnumerator SpawnProjectile() 
    {
        while (m_projectilesSpawned < m_spawnLimit) 
        {
            yield return new WaitForSeconds(m_projectileSpawnInterval);
            // spawn projectile at a random position within [m_spawnRadius]
            m_spawner?.Spawn(m_toSpawn);
            m_projectilesSpawned++;
        }
    }
    public void SpawnRewardProjectiles() 
    {
        if (m_rewardList.Count < 1) return;

        int indexToSpawn;
        int numToSpawn = Random.Range(1, 3);
        for (int i = 0; i < numToSpawn; i++)
        {
            indexToSpawn = Random.Range(0, m_rewardList.Count - 1);
            m_spawner?.Spawn(m_rewardList[indexToSpawn]);
        }
    }
    public void AttackBase() 
    {
        // Play sequence
        if (m_anim.state != PlayState.Playing) 
        {
            m_anim?.Play();
        }
        m_currentHp--;
        m_hpBar.SetBarValue(m_currentHp);
        if (m_currentHp <= 0) 
        {
            OnGameOver();        
        }
    }
    void OnGameOver() 
    {
        GameOver?.Invoke();
    }
}
