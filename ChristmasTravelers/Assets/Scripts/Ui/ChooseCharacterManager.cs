using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;
using UnityEngine.UI;

public class ChooseCharacterManager : MonoBehaviour
{

    public static ChooseCharacterManager instance;

    private GameObject[] allCharacters;
    private GameManager gameManager;


    [SerializeField] Transform canvas;
    [SerializeField] GameObject characterUiMini;
    [SerializeField] GameObject characterUiBig;
    [SerializeField] GameObject playerPool;

    private Transform allCharactersPool;
    private Transform allPlayersPool;

    private List<GameObject> playersPool;

    private int nbOfPlayers;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {

        nbOfPlayers = InputSystem.devices.OfType<XInputController>().Count();

        gameManager = GameManager.instance;

        for (int i = 1; i < nbOfPlayers; i++){
            Player newPlayer = new Player
            {
                name = "Player" + i
            };
            gameManager.players.Add(newPlayer);
        }

        allCharacters = Resources.LoadAll<GameObject>("Characters");
        allCharactersPool = canvas.Find("AllCharactersPool");
        allPlayersPool = canvas.Find("AllPlayersPool");
        playersPool = new List<GameObject>();
        InitCharacterPool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void InitCharacterPool(){
        foreach (GameObject ch in allCharacters) {
            GameObject characterUi = Instantiate(characterUiMini,allCharactersPool.transform);
            characterUi.GetComponentInChildren<TextMeshProUGUI>().text = ch.name;
            characterUi.transform.SetParent(allCharactersPool);
            characterUi.GetComponent<Button>().onClick.AddListener(() => SelectCharacter(ch,1));
        }

    }

    private void SelectCharacter(GameObject character,int playerNb){

        GameObject pool;
        
        if (playersPool.Count < playerNb){
            pool = Instantiate(playerPool,allPlayersPool.transform);
            pool.transform.SetParent(allPlayersPool);
            playersPool.Add(pool);
        } else {
            pool = playersPool[playerNb-1];
        }
        GameObject characterUi = Instantiate(characterUiBig,pool.transform);
        characterUi.GetComponentInChildren<TextMeshProUGUI>().text = character.name;
        characterUi.transform.SetParent(pool.transform);
    }




}
