using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class ResultsScreenManager : MonoBehaviour
{
    public static ResultsScreenManager instance;

    private ResultsScreen subScreen;

    [SerializeField] private Button firstButton;
    public PlayerActions actions { get; private set; }

    public static void ShowResults()
    {
        SceneManager.LoadScene("Results");
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        actions = new();
        actions.Enable();
        subScreen = ResultsScreen.LoadFrom(this);
        firstButton.Select();
    }

    

    public void OnScreenEntered()
    {
     /*   Debug.Log("ENTER");
        firstButton.Select();
        actions.Enable();      */
    }
    public void OnScreenLeft()
    {
       /* Debug.Log("LEFT");
        actions.Disable();
        EventSystem.current.SetSelectedGameObject(null);*/
    }

    public void OnClose()
    {
        SceneManager.LoadScene("ChooseCharacter");
    }

    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        actions.Dispose();
    }
}

