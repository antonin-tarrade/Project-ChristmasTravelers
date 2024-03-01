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
    PlayerActions actions;

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
        subScreen = Instantiate(Resources.LoadAll<ResultsScreen>("UI").Where(s => s.IsCompatible(GameModeData.selectedMode.type)).ToArray()[0]);
        subScreen.OnScreenExit += OnSubScreenLeft;

        actions = new();
        actions.Enable();
        actions.UIScreen.OnCursorMovement.started += (CallbackContext context) => OnSubScreenEntered();

        closeButton.Select();
    }

    private void Update()
    {
        
    }

    private void OnSubScreenEntered()
    {
        actions.Disable();
        EventSystem.current.SetSelectedGameObject(null);
        subScreen.OnScreenEntered();
    }

    private void OnSubScreenLeft()
    {
        closeButton.Select();
        actions.Enable();      
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

