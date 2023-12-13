using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Player player;

    public Transform allCharacters;

    private CharacterComponent selectedButton;

    private float lastSwitchTime;
    [SerializeField] private float switchCooldown;
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
        Character ch = selectedButton.charPrefab.GetComponent<Character>();
        Debug.Log(player.name);
        player.AddCharacter(ch);
        ChooseCharacterManager.instance.OnButtonClicked(player, ch);
    }

    // Gauche = 1, Droite = 2, Haut = 3, Bas = 4
    public void SwitchButton(int direction){

        float currentTime = Time.time;

        // Check if enough time has passed since the last switch
        if (currentTime - lastSwitchTime < switchCooldown)
        {
            return; 
        }

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

        lastSwitchTime = currentTime;
    }




 
}
