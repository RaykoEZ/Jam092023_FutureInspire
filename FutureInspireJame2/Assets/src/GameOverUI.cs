using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameOverUI : MonoBehaviour 
{
    [SerializeField] EnemyWaveManager m_wave = default;
    [SerializeField] PlayerHomeBase m_base = default;
    [SerializeField] AudioSource m_gameplayBgm = default;
    [SerializeField] TimelineAsset m_gameOverSequence = default;
    [SerializeField] TimelineAsset m_victorySequence = default;
    [SerializeField] PlayableDirector m_director = default;
    [SerializeField] AudioSource m_bgm = default;
    [SerializeField] TextMeshProUGUI m_resultText = default;
    bool m_gameOver = false;
    private void Start()
    {
        m_wave.OnAllCleared += OnVictory;
        m_base.GameOver += TriggerGameOverVisual;
    }
    private void OnVictory()
    {
        if (m_gameOver) return;
        m_gameplayBgm.Stop();
        m_resultText.text = "YOU WIN! Thanks for Playing!";
        m_gameOver = true;
        m_director.playableAsset = m_victorySequence;
        m_bgm?.Play();
        m_director?.Play();
    }
    private void TriggerGameOverVisual()
    {
        // if already game over, don't game over again
        if (m_gameOver) return;
        m_gameplayBgm.Stop();
        m_resultText.text = "GAME OVER";
        m_gameOver = true;
        m_director.playableAsset = m_gameOverSequence;
        m_bgm?.Play();
        m_director?.Play();
    }
}
