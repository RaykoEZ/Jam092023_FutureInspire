using UnityEngine;

public static class GameUtil 
{
    // Spawn a gameobject a prefab reference (preferrably)
    public static T SpawnObject<T>(T spawnRef, Vector3 position, Transform parent = null) where T : MonoBehaviour
    {
        T ret = Object.Instantiate(spawnRef);
        ret.transform.SetParent(parent, false);
        ret.transform.position = position;
        return ret;
    }
    public static Vector3 RandomPositionInBounds(Bounds bounds) 
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }
    public static Vector2 RandomPositionInBounds2D(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
}
