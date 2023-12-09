using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour
{
    public Player player;

    private PlayerInput playerInput;

    private void Start(){
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnCharacterButtonClicked(){
        int index = playerInput.playerIndex;
        Debug.Log("Button clicked by player " + (index + 1));
    }


}
