using Curry.UI;
using Curry.Util;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

// For anything that can take a hit from something
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
    [SerializeField] protected float m_pushPower = default;
    [SerializeField] protected AudioManager m_hitSfx = default;
    [SerializeField] protected AudioManager m_bounceSfx = default;
    [SerializeField] protected AudioManager m_LaunchSfx = default;
    protected Collider2D Collider => GetComponent<Collider2D>();
    protected Rigidbody2D Rb2d => GetComponent<Rigidbody2D>();
    // Log player cursor movement when dragging, we need this for getting launch direction
    Vector3 m_currentPos;
    Vector3 m_prevPos;
    bool m_followMouse = false;
    private void Start()
    {
        Collider.isTrigger = true;
    }
    void OnCollisionEnter2D(Collision2D col) 
    {
        // push pushable target
        if (col.gameObject.TryGetComponent(out IPushable push))
        {
            OnPush(push);
        }
        else 
        {
            // just play rebound sfx if we didn't hit a target
            m_bounceSfx?.PlayRandom(0.8f);
        }
        if (col.contacts.Length > 0) 
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

    protected virtual void OnPush(IPushable push) 
    {
        m_hitSfx?.PlayRandom();
        push.Push(Rb2d.velocity.normalized, m_pushPower);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        m_followMouse = true;
        Collider.isTrigger = true;
        Rb2d.velocity = Vector2.zero;
        base.OnBeginDrag(eventData);
        m_currentPos = eventData.pressEventCamera.ScreenToWorldPoint(eventData.position);
        StartCoroutine(FollowMouse());
    }
    public override void OnDrag(PointerEventData eventData)
    {
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        Vector2 dir = m_currentPos - m_prevPos;
        //if player pointer movement is idle, do not launch
        if (Vector2.SqrMagnitude(dir) > 0.01f) 
        {
            Collider.isTrigger = false;
            Launch(dir.normalized);
        }
        m_followMouse = false;
    }
    IEnumerator FollowMouse() 
    {
        while (m_followMouse) 
        {
            yield return new WaitForEndOfFrame();
            m_prevPos = m_currentPos;
            m_currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //mouse pos z is unwanted
            m_currentPos.z = transform.position.z;
            UpdatePosition(m_currentPos);
        }
    }
    public virtual void Launch(Vector2 dirNormalized, float power = 1000f) 
    {
        // a fixed chance to proc a launch sfx
        m_LaunchSfx?.PlayRandom(0.8f);
        // switch on collision
        Collider.isTrigger = false;
        Rb2d.velocity = dirNormalized * power;
    }
    protected virtual void Rebound(Vector2 normal, Vector2 relativeV) 
    {
        Vector2 v = Rb2d.velocity;
        Vector2 reflect = v - (2f * Vector2.Dot(v, normal)) * normal;
        Rb2d.velocity = reflect + 0.8f * relativeV;
    }
}
