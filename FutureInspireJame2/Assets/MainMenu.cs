using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Level1_prototype");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
