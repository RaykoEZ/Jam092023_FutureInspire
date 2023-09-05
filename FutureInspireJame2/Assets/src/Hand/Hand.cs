using System.Collections;
using UnityEngine;
public class Hand : MonoBehaviour 
{
    [SerializeField] HandProperty m_handProperty = default;
    DropItem m_itemInHand;
    Coroutine m_burning;
    public DropItem ItemInHand => m_itemInHand;

    public void OnDropItemLeave(DropItem item) 
    {
        StopCoroutine(m_burning);
        m_itemInHand = null;
    }
    public void OnDropItemLand(DropItem item) 
    {
        if (ItemInHand == item)
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
        while(ItemInHand != null) 
        {
            TakeBurnDamage(ItemInHand.Property.BurnDamagePerTick);
            yield return new WaitForSeconds(m_handProperty.BurnTimeInterval);
        }
    }
}
