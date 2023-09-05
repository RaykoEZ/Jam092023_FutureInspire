using UnityEngine;

public static class Spawner
{
    public static T SpawnObject<T>(T spawnRef, Vector3 position, Transform parent = null) where T : MonoBehaviour
    {
        T ret = Object.Instantiate(spawnRef);
        ret.transform.SetParent(parent, false);
        ret.transform.position = position;
        return ret;
    }
}
