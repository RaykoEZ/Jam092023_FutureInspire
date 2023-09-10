using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PointEffector2D))]
public class PoopAura : MonoBehaviour 
{
    [SerializeField] float m_auraLife = default;
    [SerializeField] AudioSource m_audio = default;
    protected PointEffector2D Effector => gameObject.GetComponent<PointEffector2D>();
    delegate void OnAuraExpire();
    event OnAuraExpire OnExpire; 
    private void Start()
    {
        OnExpire += OnAuraEnd;
        StartCoroutine(StartAura());
    }
    // Start aura effect
    IEnumerator StartAura() 
    {
        yield return new WaitForSeconds(0.2f);
        m_audio?.Play();
        Effector.enabled = true;
        // do effect until aura durattion ends
        yield return new WaitForSeconds(m_auraLife);
        Effector.enabled = false;
        OnExpire?.Invoke();
    }
    // end aura
    void OnAuraEnd() 
    {
        OnExpire -= OnAuraEnd;
        Destroy(gameObject);
    }
}
