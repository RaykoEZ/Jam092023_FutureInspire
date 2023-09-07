using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveStart : MonoBehaviour
{
    [SerializeField] Animator m_anim = default;
    [SerializeField] TextMeshProUGUI m_textField = default;
    [SerializeField] EnemyWaveManager m_wave = default;

    private void Start()
    {
        m_wave.OnStart += WaveStartAlert;
    }
    void WaveStartAlert(int waveNumber) 
    {
        m_textField.text = $"WAVE {waveNumber} START!";
        m_anim?.ResetTrigger("show");
        m_anim?.SetTrigger("show");
    }
}
