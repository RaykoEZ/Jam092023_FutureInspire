using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public delegate void OnEnemyDefeat(Enemy defeated);
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IPushable
{
    [SerializeField] float m_moveInterval = default;
    [SerializeField] TimelineAsset m_defeat = default;
    [SerializeField] protected PlayableDirector m_director = default;

    Coroutine m_movement;
    protected Transform m_target;
    public event OnEnemyDefeat OnDefeat;
    public void Init(Transform target) 
    {
        m_target = target;
        StartMoving();
    }
    public void Push(Vector2 dirNormalize, float power)
    {
        StartCoroutine(Push_Internal(dirNormalize, power));
    }
    public void OnDefeated() 
    {
        StopMoving();
        OnDefeat?.Invoke(this);
        StartCoroutine(Defeat_Internal());
    }
    IEnumerator Defeat_Internal() 
    {
        m_director.playableAsset = m_defeat;
        m_director.Play();
        yield return new WaitForSeconds((float)m_defeat.duration);
        Destroy(gameObject);

    }
    public void StartMoving() 
    {
        if(m_movement == null && m_target != null) 
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
    protected virtual IEnumerator Push_Internal(Vector2 dirNormalize, float power) 
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
