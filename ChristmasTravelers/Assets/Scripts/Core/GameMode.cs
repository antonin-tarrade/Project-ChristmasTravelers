using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    [field : SerializeField] public string modeName {get; private set;}

    [field : SerializeField] public int nbOfPlayers {get; private set;}

    [field : SerializeField] public int charPerPlayer {get; private set;}

    [field : SerializeField] public string scene { get ; private set; }


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



