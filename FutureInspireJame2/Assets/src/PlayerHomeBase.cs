using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerHomeBase : MonoBehaviour
{
    [SerializeField] float m_projectileSpawnInterval = default;
    [SerializeField] int m_spawnLimit = default;
    [SerializeField] DraggableProjectile m_toSpawn = default;
    [SerializeField] float m_spawnRadius = default;
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
            // sample a random position around home base
            float randomRadius = Random.Range(2f, m_spawnRadius);
            Vector3 randomSpawnPos = Random.insideUnitCircle * randomRadius;
            // spawn projectile at a random position within [m_spawnRadius]
            Spawner.SpawnObject(m_toSpawn, transform.position + randomSpawnPos, transform.parent);
            m_projectilesSpawned++;
            yield return new WaitForSeconds(m_projectileSpawnInterval);
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
