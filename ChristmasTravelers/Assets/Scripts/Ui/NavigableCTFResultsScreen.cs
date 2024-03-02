using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NavigableCTFResultsScreen : NavigableResultsScreen
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button showResultsButton;
    public override void Display(Player[] rankedPlayers)
    {
        text.text = "Winner is " + rankedPlayers[0].name;
    }

    protected override void OnCursorMovement(Vector2 m)
    {
        OnScreenLeft();
    }

    protected override void OnScreenEntered()
    {
        base.OnScreenEntered();
        showResultsButton.Select();
    }

}
