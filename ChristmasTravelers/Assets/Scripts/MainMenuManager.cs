using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("ChooseGameMode");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
