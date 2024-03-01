using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public abstract class ResultsScreen : MonoBehaviour
{
    public event Action OnScreenExit;

    private PlayerActions actions;

    /// <summary>
    /// Tells if the prefab can display results for a certain game mode
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public abstract bool IsCompatible(GameModeData.Type type);

    /// <summary>
    /// The players ordered by their score
    /// </summary>
    protected List<Player> rankedPlayers;

    private void Start()
    {
        rankedPlayers = GameModeData.selectedMode.players.OrderBy(p => p.score).ToList();
        actions = new();
        actions.UIScreen.OnCursorMovement.started += (CallbackContext context) => OnCursorMovement(context.ReadValue<Vector2>());
    }

    /// <summary>
    /// How to display the elements associated with the players
    /// </summary>
    /// <param name="rankedPlayers"></param>
    public abstract void Display(Player[] rankedPlayers);

    /// <summary>
    /// What to do when selector enters the screen (for example which button to select)
    /// </summary>
    public virtual void OnScreenEntered()
    {
        actions.Enable();
    }

    /// <summary>
    /// To call when the screen is exited via the cursor
    /// </summary>
    protected virtual void OnScreenLeft()
    {
        actions.Disable();
        OnScreenExit?.Invoke();
    }

    /// <summary>
    /// Used to navigate throughout the screen
    /// </summary>
    protected abstract void OnCursorMovement(Vector2 m);


    private void OnDestroy()
    {
        actions.Dispose();
    }
}
