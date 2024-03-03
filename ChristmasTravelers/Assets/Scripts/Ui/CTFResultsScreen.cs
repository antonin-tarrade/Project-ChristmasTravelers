using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CTFResultsScreen : ResultsScreen
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Transform CharacterContainer;
    [SerializeField] private GameObject ResultCharacterPrefab;

    public override void Display(Player[] rankedPlayers)
    {
        text.text = rankedPlayers[0].name + " won !";
        }


}
