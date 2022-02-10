using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager acc;

    private void Awake()
    {
        if (acc == null)
        {
            acc = this;
        }
        else
        {
            Debug.LogWarning("Dude 2 MainMenuManagers");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
}
