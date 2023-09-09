using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class WaveStart : MonoBehaviour
{
    [SerializeField] PlayableDirector m_director = default;
    [SerializeField] TextMeshProUGUI m_textField = default;
    [SerializeField] EnemyWaveManager m_wave = default;

    private void Start()
    {
        m_wave.OnStart += WaveStartAlert;
    }
    void WaveStartAlert(int waveNumber) 
    {
        m_textField.text = $"WAVE {waveNumber} START!";
        m_director?.Play();
    }
}
