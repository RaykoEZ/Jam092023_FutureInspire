using System.Collections;
using UnityEngine;
public class Hand : MonoBehaviour 
{
    [SerializeField] HandProperty m_handProperty = default;
    DropItem m_itemInHand;
    Coroutine m_burning;
    // Launch item with player mouse swing direction
    public void LaunchItem(float xInfluence)
    {
        if (m_itemInHand == null) return;
        Vector2 v = m_itemInHand.GetComponent<Rigidbody2D>().velocity;
        Vector2 reflect = v - (2f * Vector2.Dot(v, Vector2.up)) * Vector2.up;
        // Add in horizontal direction influence from player pointer motion
        reflect.x += 5f * xInfluence;
        m_itemInHand?.Launch(reflect, m_handProperty.MaxLaunchPower);
        OnDropItemLeave(m_itemInHand);
    }
    public void OnDropItemLeave(DropItem item) 
    {
        StopCoroutine(m_burning);
        m_itemInHand = null;
    }
    public void OnDropItemLand(DropItem item) 
    {
        if (m_itemInHand == item)
        {
            return;
        }
        m_itemInHand = item;
        m_burning = StartCoroutine(BurningHand());
    }

    // not important right now, TODO: reduce hp and proc animation
    void TakeBurnDamage(int damage) 
    {
    }
    IEnumerator BurningHand() 
    {
        while(m_itemInHand != null) 
        {
            TakeBurnDamage(m_itemInHand.Property.BurnDamagePerTick);
            yield return new WaitForSeconds(m_handProperty.BurnTimeInterval);
        }
    }
}
