using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public abstract class ResultsScreen : MonoBehaviour
{
    public static ResultsScreen LoadFrom(ResultsScreenManager mainScreen)
    {
        ResultsScreen subScreen = Instantiate(Resources.LoadAll<ResultsScreen>("UI").Where(s => s.IsCompatible(GameModeData.selectedMode.type)).ToArray()[0]);
        subScreen.AssociateTo(mainScreen);
        return subScreen;
    }

    [Tooltip("The type of game that this result screen is for")]
    [SerializeField] protected GameModeData.Type type;

    private Player[] rankedPlayers;
    protected ResultsScreenManager mainScreen;

    protected virtual void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        rankedPlayers = GameModeData.selectedMode.players.OrderBy(p => p.score).Reverse().ToArray();
        Display(rankedPlayers);
    }

    /// <summary>
    /// Tells if the prefab can display results for a certain game mode
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool IsCompatible(GameModeData.Type type) => type == this.type;

    /// <summary>
    /// How to display the elements associated with the players
    /// </summary>
    /// <param name="rankedPlayers">Players ordered by score</param>
    public abstract void Display(Player[] rankedPlayers);

    protected virtual void AssociateTo(ResultsScreenManager mainScreen)
    {
        this.mainScreen = mainScreen;
    }



}
