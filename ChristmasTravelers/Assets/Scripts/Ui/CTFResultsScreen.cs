using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CTFResultsScreen : ResultsScreen
{
    [SerializeField] private TextMeshProUGUI text;
    public override void Display(Player[] rankedPlayers)
    {
        text.text = "Winner is " + rankedPlayers[0];
    }
}
