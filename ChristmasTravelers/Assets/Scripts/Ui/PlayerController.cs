using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Player player{get; set;}

    public Transform allCharacters;

    private CharacterComponent[][] positions;

    private CharacterComponent selectedButton;
    // Start is called before the first frame update
    void Start()
    {
        allCharacters = GameObject.Find("AllCharactersPool").transform;
        selectedButton = allCharacters.GetChild(0).GetComponent<CharacterComponent>();
        selectedButton.OnSelect(player);
        positions = ChooseCharacterManager.matrice;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClicked() {
        player.AddCharacter(selectedButton.charPrefab.GetComponent<Character>());
    }

    private void SwitchButtonRight()
    {
        selectedButton ??= positions[selectedButton.position.Item1][selectedButton.position.Item2+1];
    }

    private void SwitchButtonLeft()
    {
        selectedButton ??= positions[selectedButton.position.Item1][selectedButton.position.Item2-1];
    }

    private void SwitchButtonUp(){
        selectedButton ??= positions[selectedButton.position.Item1-1][selectedButton.position.Item2];
    }

    private void SwitchButtonDown(){
        selectedButton ??= positions[selectedButton.position.Item1+1][selectedButton.position.Item2];
    }


    // Gauche = 1, Droite = 2, Haut = 3, Bas = 4
    public void SwitchButton(int direction){
        selectedButton.OnDeselect();

        switch (direction) {
            case 1 :
                SwitchButtonLeft();
                break;
            case 2 : 
                SwitchButtonRight();
                break;
            case 3 : 
                SwitchButtonUp();
                break;
            case 4 : 
                SwitchButtonDown();
                break;
        }

        selectedButton.OnSelect(player);
    }




 
}
