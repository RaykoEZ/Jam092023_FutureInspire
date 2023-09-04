using UnityEngine;

public delegate void OnDropItemLand(DropItem item);
[RequireComponent(typeof(Collider2D))]
public class DropItemDetector : MonoBehaviour
{
    public event OnDropItemLand OnItemLanded;
    public event OnDropItemLand OnItemLeave;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out DropItem item)) 
        {
            OnItemLanded?.Invoke(item);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out DropItem item))
        {
            OnItemLanded?.Invoke(item);
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DropItem item))
        {
            OnItemLeave?.Invoke(item);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out DropItem item))
        {
            OnItemLeave?.Invoke(item);
        }
    }
}
