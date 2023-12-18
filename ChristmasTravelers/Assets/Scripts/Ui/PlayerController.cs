using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering;
using UnityEngine.U2D;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    public Player player;

    [SerializeField] private Transform allCharacters;

    private CharacterComponent selectedButton;

    [SerializeField] private GameObject genericSelector;
    public GameObject selector { get; private set; }

    // Input fields
    private float lastSwitchTime;
    [SerializeField] private float switchCooldown;

    private bool buttonEnabled = true;
    [SerializeField]  private float clickCooldown;



    // Start is called before the first frame update
    void Start()
    {
        allCharacters = GameObject.Find("AllCharactersPool").transform;

        InitSelector();
        InitSelectedButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void InitSelector()
    {
        selector = Instantiate(genericSelector, transform);
        TextMeshProUGUI tmp = selector.GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = "P" + player.number;
        tmp.color = player.color;

        RectTransform corners = selector.transform.GetChild(0) as RectTransform;
        foreach (RectTransform corner in corners)
        {
            foreach(RectTransform cornerPart in corner)
            {
                cornerPart.GetComponent<Image>().color = player.color;
            }
        }
    }

    private void InitSelectedButton()
    {
        selectedButton = allCharacters.GetChild(0).GetComponent<CharacterComponent>();
        GoToSelectedButton(selectedButton.transform.position);
    }

    public void OnButtonClicked() {
        if (selectedButton == null || !buttonEnabled )
        {
            return;
        }

        StartCoroutine(ButtonCooldown());

        Character ch = selectedButton.charPrefab.GetComponent<Character>();
        player.AddCharacter(ch);
        ChooseCharacterManager.instance.OnCharacterAdded(player, ch);
    }

    IEnumerator ButtonCooldown()
    {
        buttonEnabled = false;
        yield return new WaitForSeconds(clickCooldown);

        buttonEnabled = true;
    }
    
    public void RemoveLastAddedChar()
    {
        if (player.characters.Count == 0 || !buttonEnabled)
        {
            return;
        }
        StartCoroutine(ButtonCooldown());
        player.characters.RemoveAt(player.characters.Count - 1);
        ChooseCharacterManager.instance.OnCharacterDeleted(player);
    }



    // Gauche = 1, Droite = 2, Haut = 3, Bas = 4
    public void SwitchButton(int direction){

        float currentTime = Time.time;

        // Check if enough time has passed since the last switch
        if (currentTime - lastSwitchTime < switchCooldown)
        {
            return; 
        }

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

        GoToSelectedButton(selectedButton.transform.position);
        lastSwitchTime = currentTime;
    }


    private void GoToSelectedButton(Vector3 buttonPosition)
    {
        selector.transform.position = buttonPosition;
    }


}
