using UnityEngine;
public class DropItem : MonoBehaviour 
{
    [SerializeField] private ItemProperty itemProperty = default;
    public ItemProperty Property => itemProperty;
    Rigidbody2D rb2d => GetComponent<Rigidbody2D>();
    public void Launch(Vector2 dir, float launchPower = 1f) 
    {
        Debug.Log("Launch");
        rb2d.AddForce(dir * launchPower, ForceMode2D.Impulse);
    }

}
