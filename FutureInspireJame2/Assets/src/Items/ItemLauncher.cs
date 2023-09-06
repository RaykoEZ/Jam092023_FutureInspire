using UnityEngine;

public class ItemLauncher : MonoBehaviour 
{
    [SerializeField] float m_maxLaunchPower;
    public float MaxLaunchPower => m_maxLaunchPower;
    // Launch item with player mouse swing direction
    public void LaunchItem(DropItem item, float power, float xInfluence)
    {
        if (item == null) return;
        Vector2 v = item.GetComponent<Rigidbody2D>().velocity;
        Vector2 reflect = v - (2f * Vector2.Dot(v, Vector2.up)) * Vector2.up;
        // Add in horizontal direction influence from player pointer motion
        reflect.x += 5f * xInfluence;
        item?.Launch(reflect, Mathf.Clamp(power, 0.1f, m_maxLaunchPower));
    }
}
