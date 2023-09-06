using UnityEngine;

public class RangedSpawner : MonoBehaviour 
{
    [SerializeField] Collider2D m_spawnRange = default;
    public T Spawn<T>(T spawnRef, bool clearZ = true) where T : MonoBehaviour
    {
        Vector3 spawnPos = GameUtil.RandomPositionInBounds(m_spawnRange.bounds);
        T ret = GameUtil.SpawnObject(spawnRef, spawnPos, transform.parent);
        // To stop instance from inheriting z position to stop z fighting 
        if (clearZ) 
        {
            Vector3 pos = ret.transform.localPosition;
            pos.z = 0f;
            ret.transform.localPosition = pos;
        }
        return ret;
    }
}
