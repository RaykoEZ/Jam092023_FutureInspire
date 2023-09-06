using UnityEngine;

public delegate void OnHangingItemHit(DropItem hitBy, HangingItem hitItem);
[RequireComponent(typeof(Collider2D))]
// For all items for us to hit with our food tossing
public abstract class HangingItem : MonoBehaviour 
{
    public event OnHangingItemHit OnHit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out DropItem item)) 
        {
            OnItemHit(item);
        }
    }
    private void OnItemHit(DropItem hit)
    {
        Debug.Log($"hit by {hit.Property.Name}");
        OnHit?.Invoke(hit, this);
    }
    // implement item effects here, we call this with a manager class that has reference to our game state:
    // e.g. hands, items...etc
    public abstract void ActivateEffect(Hand hand, DropItem item);
}
