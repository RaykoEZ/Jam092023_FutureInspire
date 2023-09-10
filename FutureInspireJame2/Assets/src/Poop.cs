using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Poop : DraggableProjectile 
{
    [SerializeField] PoopAura m_auraToSpawn = default;
    protected override void OnPush(IPushable push)
    {
        // spawn aura
        Vector3 pos = transform.position;
        pos.z = 1f;
        GameUtil.SpawnObject(m_auraToSpawn, pos, transform.parent);
        Destroy(gameObject);
        
    }
}
