using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IHittable, IPushable
{
    [Range(1f, 999999f)]
    [SerializeField] int m_maxHp = default;
    // unit to move per movement call
    [Range(0.1f, 1f)]
    [SerializeField] float m_moveInterval = default;
    [SerializeField] Transform m_base = default;
    int m_currentHp;
    Coroutine m_movement;
    Transform m_target;
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    void Awake() 
    {
        m_currentHp = m_maxHp;
    }
    void Start() 
    {
        m_target = m_base;
        StartMoving();
    }
    public void Init(Transform target) 
    {
        m_target = target;
        StartMoving();
    }
    public void TakeHit(int damage)
    {
        m_currentHp -= damage; 
        if(m_currentHp <= 0) 
        {
            OnDefeated();
        }
    }
    public void Push(Vector2 dirNormalize, float power)
    {
        StartCoroutine(Push_Internal(dirNormalize, power));
    }
    public void OnDefeated() 
    {
        StopMoving();
        Destroy(gameObject);
    }

    public void StartMoving() 
    {
        if(m_movement == null) 
        {
            m_movement = StartCoroutine(Movement());
        }
    }
    public void StopMoving() 
    {
        if (m_movement != null)
        {
            StopCoroutine(m_movement);
            m_movement = null;
        }
    }
    IEnumerator Push_Internal(Vector2 dirNormalize, float power) 
    {
        StopMoving();
        GetComponent<Rigidbody2D>()?.AddForce(dirNormalize * power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        StartMoving();
    }
    IEnumerator Movement() 
    {
        float dist = Vector3.Distance(transform.position, m_target.position);
        float t = 0f;
        while (dist > 0.01f) 
        {
            t += m_moveInterval * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, m_target.position, t);
            yield return new WaitForSeconds(0.5f);
            dist = Vector3.Distance(transform.position, m_target.position);
        }
        m_movement = null;
    }
}
