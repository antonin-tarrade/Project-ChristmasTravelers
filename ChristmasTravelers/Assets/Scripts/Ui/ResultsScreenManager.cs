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

    [SerializeField] private Button closeButton;
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
        closeButton.Select();
    }

    

    public void OnScreenEntered()
    {
        Debug.Log("ENTER");
        closeButton.Select();
        actions.Enable();      
    }

    public void OnScreenLeft()
    {
        Debug.Log("LEFT");
        actions.Disable();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnClose()
    {
        SceneManager.LoadScene("ChooseGameMode");
    }

    private void OnDestroy()
    {
        actions.Dispose();
    }
}

