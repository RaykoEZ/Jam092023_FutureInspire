using UnityEngine;
using UnityEngine.Playables;

public class GameOverUI : MonoBehaviour 
{
    [SerializeField] PlayerHomeBase m_base = default;
    [SerializeField] PlayableDirector m_director = default;
    [SerializeField] AudioSource m_bgm = default;
    bool m_gameOver = false;
    private void Start()
    {
        m_base.GameOver += TriggerGameOverVisual;
    }
    void TriggerGameOverVisual()
    {
        // if already game over, don't game over again
        if (m_gameOver) return;

        m_gameOver = true;
        m_bgm?.Play();
        m_director?.Play();
    }
}
