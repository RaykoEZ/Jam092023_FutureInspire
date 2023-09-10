using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
public delegate void OnEnemyDefeat(Enemy defeated);
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IPushable
{
    [Range(0.1f, 1f)]
    [SerializeField] float m_moveInterval = default;
    Coroutine m_movement;
    Transform m_target;
    public event OnEnemyDefeat OnDefeat;
    private AudioSource audioSource;
    private PlayableDirector playableDirector;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playableDirector = GetComponent<PlayableDirector>();
    }
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
        if (playableDirector != null)
        {
            playableDirector.Play();
        }
        StartCoroutine(DestroyAfterTimeline());
    }

    public void StartMoving()
    {
        if (m_movement == null && m_target != null)
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

    IEnumerator DestroyAfterTimeline()
    {
        yield return new WaitForSeconds((float)playableDirector.duration);
        Destroy(gameObject);
    }
}
