using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float m_attackTimeInterval = default;
    PlayerHomeBase m_baseInSight;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHomeBase playerBase))
        {
            m_baseInSight = playerBase;
        }
        StartCoroutine(AttackBase());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHomeBase _)) 
        {
            m_baseInSight = null;
        }
    }
    IEnumerator AttackBase() 
    {
        while (m_baseInSight != null)
        {
            m_baseInSight.AttackBase();
            yield return new WaitForSeconds(m_attackTimeInterval);
        }
    }
}
