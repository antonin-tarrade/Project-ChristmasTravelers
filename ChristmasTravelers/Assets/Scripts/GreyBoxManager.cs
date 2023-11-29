using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreyBoxManager : MonoBehaviour
{
    private GameManager gameManager;
    private RoundManager roundManager;
    private Button changePlayerButton;
    private bool isBlueTurn = true;
    private bool curentPlayerIsBlue = true;
    private bool isFirstTurn = true;

    [Header("Button Colors")]
    public Color colorBlue;
    public Color colorRed;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        roundManager = RoundManager.instance;
        changePlayerButton = GameObject.Find("ChangePlayerButton").GetComponent<Button>();
        changePlayerButton.GetComponentInChildren<Image>().color = colorBlue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePlayer()
    {
        isBlueTurn = !isBlueTurn;
        if (isBlueTurn)
        {
            changePlayerButton.GetComponentInChildren<Image>().color = colorBlue;
        }
        else
        {
            changePlayerButton.GetComponentInChildren<Image>().color = colorRed;
        }
    }


    public void StartNextTurn()
    {
        if (isBlueTurn != curentPlayerIsBlue || isFirstTurn){
            roundManager.StartNextTurn();
            
            if (isFirstTurn) {
                isFirstTurn = false;
            } else {
                curentPlayerIsBlue = !curentPlayerIsBlue;
            }
        } else {
            roundManager.StartSameTurn();
        }

        
    }
}
