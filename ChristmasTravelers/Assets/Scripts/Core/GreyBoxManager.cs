using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreyBoxManager : MonoBehaviour
{
    private GameManager gameManager;
    private Button changePlayerButton;
    private bool isBlueSelected = true;
    private bool curentPlayerIsBlue = true;
    private bool isFirstTurn = true;

    [Header("Button Colors")]
    public Color colorBlue;
    public Color colorRed;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        changePlayerButton = GameObject.Find("ChangePlayerButton").GetComponent<Button>();
        changePlayerButton.GetComponentInChildren<Image>().color = colorBlue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePlayer()
    {
        changePlayerButton.GetComponentInChildren<Image>().color = (isBlueSelected) ? colorRed : colorBlue;
        isBlueSelected = !isBlueSelected;
    }


    public void StartNextTurn()
    {
        if (isBlueSelected!= curentPlayerIsBlue || isFirstTurn){
            //gameManager.StartTurn();
            
            if (isFirstTurn) {
                isFirstTurn = false;
            } else {
                curentPlayerIsBlue = !curentPlayerIsBlue;
            }
        } else {
            //gameManager.StartTurn();
        }

        
    }
}
