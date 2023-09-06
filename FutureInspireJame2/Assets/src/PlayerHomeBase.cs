using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerHomeBase : MonoBehaviour
{
    [SerializeField] float m_projectileSpawnInterval = default;
    [SerializeField] int m_spawnLimit = default;
    [SerializeField] DraggableProjectile m_toSpawn = default;
    [SerializeField] RangedSpawner m_spawner = default;
    int m_projectilesSpawned = 0;
    // Start is called before the first frame update
    void Start()
    {
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnBaseAttacked();
    }
    void OnBaseAttacked() 
    {
    }
}
