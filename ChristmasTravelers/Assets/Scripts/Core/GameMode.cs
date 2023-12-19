using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    [SerializeField] private string modeName;
    public string ModeName { get => modeName; protected set => _ = modeName; }

    [SerializeField] private int nbOfPlayers;
    public int NbOfPlayers { get => nbOfPlayers; protected set => _ = nbOfPlayers; }

    [SerializeField] private int charPerPlayer;
    public int CharPerPlayer { get => charPerPlayer; protected set => _ = charPerPlayer; }

    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.instance;

        GetComponentInChildren<TextMeshProUGUI>().text = modeName;
        GetComponent<Button>().onClick.AddListener(() => SelectGameMode());
    }


    public void SelectGameMode()
    {
        gameManager.gameMode = this;
    }

}



