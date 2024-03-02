using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public abstract class NavigableResultsScreen : ResultsScreen
{
    private PlayerActions actions;
    private bool ignoreNextInput;

    protected override void Awake()
    {
        base.Awake();
        actions = new();
        actions.Disable();
        actions.UIScreen.OnCursorMovement.started +=
            (CallbackContext context) =>
            {
                if (ignoreNextInput) ignoreNextInput = false;
                else OnCursorMovement(context.ReadValue<Vector2>());
            };
    }

    protected override sealed void AssociateTo(ResultsScreenManager mainScreen)
    {
        this.mainScreen = mainScreen;
        mainScreen.actions.UIScreen.OnCursorMovement.started +=
            (CallbackContext context) =>
            {
                if (ignoreNextInput) ignoreNextInput = false;
                else OnScreenEntered();
            };
    }

    /// <summary>
    /// Called when navigating throughout the screen. 
    /// Can be used to determine whether to exit the sub screen or not.
    /// </summary>
    protected abstract void OnCursorMovement(Vector2 m);

    /// <summary>
    /// What to do when selector enters the screen (for example which button to select)
    /// </summary>
    protected virtual void OnScreenEntered()
    {
        Debug.Log("SUB ENTER");
        mainScreen.OnScreenLeft();
        ignoreNextInput = true;
        actions.Enable();
    }

    /// <summary>
    /// To call when the screen is exited via the cursor movement
    /// </summary>
    protected virtual void OnScreenLeft()
    {
        Debug.Log("SUB LEFT");
        mainScreen.OnScreenEntered();
        ignoreNextInput = true;
        actions.Disable();
    }
}
