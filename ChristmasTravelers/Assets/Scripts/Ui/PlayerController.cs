using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;

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

        bool isFull = player.characters.Count >= gameManager.gameMode.CharPerPlayer;

        if (selectedButton == null || !buttonEnabled || isFull)
        {
            return;
        }

        StartCoroutine(ButtonCooldown());

        Character ch = selectedButton.charPrefab.GetComponent<Character>();
        ChooseCharacterManager.instance.OnCharacterAdded(player, ch);
        player.AddCharacter(ch);
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
        ChooseCharacterManager.instance.OnCharacterDeleted(player);
        player.characters.RemoveAt(player.characters.Count - 1);
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
