using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private RectTransform playerContainer;

    private Transform allCharactersPool;
    private Transform allPlayersPool;

    private Dictionary<Player, GameObject> playersPool;

    private int nbOfPlayers;

    private int charPerPlayer;

    [SerializeField] private int maxColumns;

    [Header("Debug")]
    [SerializeField]private Color[] couleurs; //debug

    public static CharacterComponent[][] matrice{get; private set;}

    private bool[] playersCanBeReady;
    private bool[] playersReady;
    
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

        gameManager = GameManager.instance;

        nbOfPlayers = gameManager.gameMode.NbOfPlayers;
        charPerPlayer = gameManager.gameMode.CharPerPlayer;

        playersCanBeReady = new bool[nbOfPlayers];
        playersReady = new bool[nbOfPlayers];

        playersPool = new Dictionary<Player, GameObject>();

        allCharacters = Resources.LoadAll<GameObject>("Characters").Where(ch => !ch.name.StartsWith('[')).ToArray();
        allCharactersPool = canvas.Find("AllCharactersPool");
        allPlayersPool = canvas.Find("AllPlayersPool");

        InitPlayerPool();
        InitCharacterPool();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void InitCharacterPool(){

        GridLayoutGroup grid = allCharactersPool.GetComponent<GridLayoutGroup>();
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = maxColumns;

        int nbRow = (allCharacters.Length + maxColumns - 1) / maxColumns ;

        matrice = new CharacterComponent[nbRow][];
        for (int i = 0; i < nbRow ; i++){
            matrice[i] = new CharacterComponent[maxColumns];
        }

        int currentCol = 0;
        int currentRow = 0;
        foreach (GameObject ch in allCharacters) {

            GameObject characterUi = Instantiate(characterUiMini,allCharactersPool.transform);

            characterUi.name = ch.name;
            characterUi.GetComponentInChildren<TextMeshProUGUI>().text = ch.name;
            characterUi.transform.SetParent(allCharactersPool);
            if (currentCol >= maxColumns){
                currentCol = 0;
                currentRow ++;
            } 
            CharacterComponent component = characterUi.GetComponent<CharacterComponent>();
            matrice[currentRow][currentCol] = component;
            component.charPrefab = ch;
            currentCol++;
        }

        int total = 0;
        for (int i = 0;i < nbRow ; i++)
        {
            for (int j = 0; j < maxColumns && total < allCharacters.Length; j++)
            {  
                matrice[i][j].right = (j + 1 == maxColumns || matrice[i][j+1] == null) ? matrice[i][0] : matrice[i][j + 1];
                if (j == 0) {
                    int last = maxColumns -1 ;
                    while (matrice[i][last] == null){
                         last--;
                    }
                    matrice[i][j].left = matrice[i][last];
                } else {
                    matrice[i][j].left = matrice[i][j - 1];
                }
                matrice[i][j].up = (i == 0) ? ((matrice[nbRow -1][j] == null) ? matrice[nbRow-1][0] : matrice[nbRow - 1][j]) : matrice[i - 1][j]; 
                matrice[i][j].down = (i + 1 == nbRow) ? matrice[0][j] :  ((matrice[i + 1][j] == null) ? matrice[i+1][0]: matrice[i + 1][j]);
                total++;
            }
        }

    }


    private void InitPlayerPool(){
        for (int i = 0; i< nbOfPlayers; i++){
            GameObject pPool = Instantiate(playerPool,allPlayersPool.transform);
            pPool.name = "UnsetPool";
            pPool.transform.SetParent(allPlayersPool);


            Transform pool = pPool.transform.GetChild(1);
            for (int j = 0; j<charPerPlayer; j++){
                GameObject placeHolder = Instantiate(characterUiBig, pool.transform);
                InitCharPlaceHolder(placeHolder);
            }
        }
    }

    private void InitCharPlaceHolder(GameObject placeHolder){
        placeHolder.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f,0.5f);
        placeHolder.GetComponentInChildren<TextMeshProUGUI>().text = "Choose a Character";
    }


    public void OnPlayerJoined (){

        // Si possible trouver une autre maniere de recuperer le playerController (Spawn par l'inputManager)
        PlayerController pc = GameObject.Find("PlayerController(Clone)").GetComponent<PlayerController>();

        // Instanciate the player
        int playerNumber = gameManager.players.Count;
        Player newPlayer = new Player
        {
            name = "Player " + (playerNumber + 1),
            color = couleurs[playerNumber],
            number = playerNumber + 1

        };
        newPlayer.Init();
        gameManager.players.Add(newPlayer);

        // Player Pool
        Transform pool = allPlayersPool.Find("UnsetPool");
        UpdatePlayerPool(pool, newPlayer);

        // Player Controller
        pc.player = newPlayer;
        pc.name = newPlayer.name;
        pc.transform.SetParent(playerContainer, false);

        playersPool.Add(newPlayer, pool.gameObject);
    }


    private void UpdatePlayerPool(Transform pool, Player player)
    {
        pool.name = player.name + "Pool";
        TextMeshProUGUI tmp = pool.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        tmp.text = player.name;
        tmp.color = player.color;
        TextMeshProUGUI readyText = pool.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        readyText.text = "Not Ready";
        readyText.color = Color.red;
    }


    public void OnCharacterAdded(Player player,Character character)
    {
        playersPool.TryGetValue(player, out GameObject pool);
        Transform charUI = pool.transform.GetChild(1).GetChild(player.characters.Count);
        charUI.GetComponentInChildren<TextMeshProUGUI>().text = character.name;
        charUI.GetComponent<Image>().color = player.color;

        if (player.characters.Count == gameManager.gameMode.CharPerPlayer - 1) {
            pool.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text += "\n press start";
            playersCanBeReady[player.number-1] = true;
        }

    }

    public void OnCharacterDeleted(Player player)
    {
        playersPool.TryGetValue(player, out GameObject pool);
        InitCharPlaceHolder(pool.transform.GetChild(1).GetChild(player.characters.Count - 1).gameObject);
    }

    public void OnReady(Player player){
        playersReady[player.number - 1 ] = playersCanBeReady[player.number - 1];
        if (IsEveryPlayerReady()) {
            SceneManager.LoadScene(gameManager.gameMode.Scene);
        }
    }

    private bool IsEveryPlayerReady(){
        bool isEveryoneRdy = true;
        foreach (bool b in playersReady){
            isEveryoneRdy = isEveryoneRdy || b;
        }
        return isEveryoneRdy;
    }

}
