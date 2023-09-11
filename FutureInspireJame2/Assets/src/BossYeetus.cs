using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class BossYeetus : Enemy , IPushable
{
    [SerializeField] float m_abilityCooldown = default;
    [SerializeField] float m_chargeDuration = default;
    [SerializeField] float m_abilityRadius = default;
    [SerializeField] float m_launchPower = default;
    [SerializeField] Animator m_anim = default;
    [SerializeField] AudioSource m_chargingSFX = default;
    float m_abilityTimer = 0f;
    static readonly string[] s_enemyCheckLayer = new string[] { "Enemy"};
    protected void Start() 
    {
        StartCoroutine(AbilityLoop());
    }
    public void Yeet() 
    {
        Vector2 dir = (m_target.position - transform.position).normalized;
        Vector2 resultDir;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, m_abilityRadius, Vector2.zero, 
            0f, LayerMask.GetMask(s_enemyCheckLayer));
        float power;
        bool isPlayer;
        foreach (var hit in hits) 
        {           
            if (hit.collider.gameObject != gameObject && hit.collider.TryGetComponent(out IPushable push)) 
            {
                if(push is PlayerMovement) 
                {
                    power = 100000f * m_launchPower;
                    resultDir = (hit.collider.transform.position - transform.position).normalized;
                }
                else 
                {
                    power = m_launchPower;
                    resultDir = dir;
                }
                push.Push(resultDir, power);
            }
        }
        StartMoving();
    }
    IEnumerator AbilityLoop() 
    {     
        while (m_abilityTimer < m_abilityCooldown)
        {
            yield return new WaitForEndOfFrame();
            m_abilityTimer += Time.deltaTime;
        }
        m_abilityTimer = 0f;
        yield return StartCharging();
    }
    IEnumerator ActivateAbility() 
    {
        m_director?.Play();
        yield return new WaitForSeconds(5f);
        yield return StartCoroutine(AbilityLoop());
    }
    IEnumerator StartCharging() 
    {
        StopMoving();
        m_anim?.SetBool("charging", true);
        m_chargingSFX?.Play();
        yield return new WaitForSeconds(m_chargeDuration);
        m_chargingSFX?.Stop();
        m_anim?.SetBool("charging", false);
        yield return ActivateAbility();
    }
}
