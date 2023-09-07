using Curry.Util;
using UnityEngine;
using UnityEngine.EventSystems;

// For anything that can take a hit from something
public interface IHittable 
{
    public void TakeHit(int damage);
}
// Anything that can be pushed away by a force
public interface IPushable 
{
    public void Push(Vector2 dir, float power);
}
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class DraggableProjectile : DraggableObject
{
    [SerializeField] int m_damage = default;
    [SerializeField] float m_pushPower = default;
    protected Collider2D Collider => GetComponent<Collider2D>();
    protected Rigidbody2D Rb2d => GetComponent<Rigidbody2D>();
    // Log player cursor movement when dragging, we need this for getting launch direction
    Vector3 m_currentPos;
    Vector3 m_prevPos;
    private void Start()
    {
        Collider.isTrigger = true;
    }
    private void OnCollisionEnter2D(Collision2D col) 
    {
        // Hittable targets get hit
        if (col.gameObject.TryGetComponent(out IHittable hit))
        {
            hit.TakeHit(m_damage);
        }
        if (col.gameObject.TryGetComponent(out IPushable push))
        {
            push.Push(Rb2d.velocity.normalized, m_pushPower);
        }
        if(col.contacts.Length > 0) 
        {
            Vector2 n = col.contacts[0].normal;
            // Project hits one target and destroys itself for now
            Rebound(n, col.relativeVelocity);
        }
        else 
        {
            Rebound(Rb2d.velocity.normalized, Vector2.zero);
        }
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (Draggable) 
        {
            m_dragging = true;
            Rb2d.velocity = Vector2.zero;
            base.OnBeginDrag(eventData);
            m_currentPos = eventData.pressEventCamera.ScreenToWorldPoint(eventData.position);
        }
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (!Draggable) { return; }
            // Do not move when drag is held, if the object needs to do something else
            base.OnDrag(eventData);
        m_prevPos = m_currentPos;
        m_currentPos = eventData.pressEventCamera.ScreenToWorldPoint(eventData.position);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (!Draggable) { return; }
        Vector2 dir = m_currentPos - m_prevPos;
        //if player pointer movement is idle, do not launch
        if(Vector2.SqrMagnitude(dir) > 0.01f) 
        {
            Launch(dir.normalized);
            Draggable = false;
        }
        else 
        { 
            Draggable = true; 
        }
    }
    public void Launch(Vector2 dirNormalized, float power = 10f) 
    {
        // switch on collision
        Collider.isTrigger = false;
        Rb2d.velocity = dirNormalized * power;
    }
    void Rebound(Vector2 normal, Vector2 relativeV) 
    {
        Vector2 v = Rb2d.velocity;
        Vector2 reflect = v - (2f * Vector2.Dot(v, normal)) * normal;
        Rb2d.velocity = reflect + 0.8f * relativeV;
    }
}
