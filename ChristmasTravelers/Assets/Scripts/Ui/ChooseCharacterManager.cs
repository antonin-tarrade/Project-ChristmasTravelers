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

    private Dictionary<Player,GameObject> playersPool;

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

        playersPool = new Dictionary<Player,GameObject>();

        nbOfPlayers = InputSystem.devices.OfType<XInputController>().Count();
        
        Debug.Log(nbOfPlayers);

        gameManager = GameManager.instance;


        for (int i = 0; i < nbOfPlayers; i++){
            Player newPlayer = new Player
            {
                name = "Player" + i + 1
            };
            gameManager.players.Add(newPlayer);

            InitPlayerPool(newPlayer);
        }

        allCharacters = Resources.LoadAll<GameObject>("Characters");
        allCharactersPool = canvas.Find("AllCharactersPool");
        allPlayersPool = canvas.Find("AllPlayersPool");
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

            foreach(Player p in gameManager.players){
                characterUi.GetComponent<Button>().onClick.AddListener(() => SelectCharacter(ch,p));
            }
        }

    }

    private void SelectCharacter(GameObject character,Player player){

        GameObject pool; 
        playersPool.TryGetValue(player,out pool);
        GameObject characterUi = Instantiate(characterUiBig,pool.transform);
        characterUi.GetComponentInChildren<TextMeshProUGUI>().text = character.name;
        characterUi.transform.SetParent(pool.transform);
    }


    private void InitPlayerPool(Player player){
        GameObject pool = Instantiate(playerPool,allPlayersPool.transform);
        pool.transform.SetParent(allPlayersPool);
        playersPool.Add(player,pool);
    }




}
