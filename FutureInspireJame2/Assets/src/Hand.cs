using System.Collections;
using UnityEngine;
public class Hand : MonoBehaviour 
{
    [SerializeField] HandProperty m_handProperty = default;
    [SerializeField] DropItemDetector m_detect = default;
    DropItem m_itemInHand;
    Coroutine m_burning;
    private void Start()
    {
        m_detect.OnItemLanded += OnDropItemLand;
    }
    public void LaunchItem()
    {
        m_itemInHand?.Launch(Vector2.up, m_handProperty.MaxLaunchPower);
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
        Debug.Log($"Taking {damage} damage.");
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
