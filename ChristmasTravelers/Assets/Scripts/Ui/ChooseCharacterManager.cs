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


    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject characterUiMini;
    [SerializeField] private GameObject characterUiBig;
    [SerializeField] private GameObject playerPool;

    private Transform allCharactersPool;
    private Transform allPlayersPool;

    private Dictionary<Player,GameObject> playersPool;

    private PlayerInputManager playerInputManager;

    [SerializeField] private int nbOfPlayers;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        playerInputManager = GetComponent<PlayerInputManager>();
        
    }


    // Start is called before the first frame update
    void Start()
    {

        playersPool = new Dictionary<Player,GameObject>();

        allCharacters = Resources.LoadAll<GameObject>("Characters");
        allCharactersPool = canvas.Find("AllCharactersPool");
        allPlayersPool = canvas.Find("AllPlayersPool");

        InitPlayerPool();

        gameManager = GameManager.instance;

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

            characterUi.GetComponent<Button>().onClick.AddListener(()=> Debug.Log(playerInputManager.maxPlayerCount));
        }

        allCharactersPool.transform.GetChild(0).GetComponent<Button>().Select();

    }


    private void SelectCharacter(GameObject character,Player player){

        playersPool.TryGetValue(player, out GameObject pool);
        GameObject characterUi = Instantiate(characterUiBig,pool.transform);
        characterUi.GetComponentInChildren<TextMeshProUGUI>().text = character.name;
        characterUi.transform.SetParent(pool.transform);
    }

    private void InitPlayerPool(){
        for (int i = 0; i< nbOfPlayers; i++){
            GameObject pool = Instantiate(playerPool,allPlayersPool.transform);
            pool.name = "UnsetPool";
            pool.transform.SetParent(allPlayersPool);
        }
    }




    public void OnPlayerJoined (){

        Player newPlayer = new Player
        {
            name = "Player" + gameManager.players.Count
        };
        gameManager.players.Add(newPlayer);
        
        Transform pool = allPlayersPool.Find("UnsetPool");
        GameObject poolGO = pool.gameObject;
        poolGO.name = newPlayer.name + "Pool";
        poolGO.GetComponent<TextMeshProUGUI>().text = newPlayer.name;
        playersPool.Add(newPlayer,poolGO);

        GameObject playerSelector = GameObject.Find("PlayerSelector(Clone)");
        playerSelector.name = newPlayer.name + "Selector";
    }


}
