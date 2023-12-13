using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Player player;

    public Transform allCharacters;

    private CharacterComponent selectedButton;
    // Start is called before the first frame update
    void Start()
    {
        allCharacters = GameObject.Find("AllCharactersPool").transform;
        selectedButton = allCharacters.GetChild(0).GetComponent<CharacterComponent>();
        selectedButton.OnSelect(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClicked() {
        player.AddCharacter(selectedButton.charPrefab.GetComponent<Character>());
    }

    // Gauche = 1, Droite = 2, Haut = 3, Bas = 4
    public void SwitchButton(int direction){


        selectedButton.OnDeselect();

        CharacterComponent button = null;

        switch (direction) {
            case 1 :
                button = selectedButton.left;
                break;
            case 2 :
                button = selectedButton.right;
                break;
            case 3 :
                button = selectedButton.up;
                break;
            case 4 :
                button = selectedButton.down;
                break;
        }

        selectedButton = button;

        selectedButton.OnSelect(player);
    }




 
}
