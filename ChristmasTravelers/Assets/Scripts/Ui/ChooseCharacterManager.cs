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

    private Dictionary<Player, PlayerPoolUI> playersPool;

    private int nbOfPlayersRequired;
    private int currentNbOfPlayers;


    private int charPerPlayer;

    [SerializeField] private int maxColumns;

    [Header("Debug")]
    [SerializeField]private Color[] couleurs; //debug

    public static CharacterComponent[][] matrice{get; private set;}
    
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

        nbOfPlayersRequired = gameManager.gameMode.NbOfPlayers;
        currentNbOfPlayers = 0;
        charPerPlayer = gameManager.gameMode.CharPerPlayer;

        playersPool = new Dictionary<Player, PlayerPoolUI>();

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

        for (int i = 0; i< nbOfPlayersRequired; i++){
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
        currentNbOfPlayers++;
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
        GameObject pool = allPlayersPool.Find("UnsetPool").gameObject;
        PlayerPoolUI playerPool = CreatePlayerPool(pool, newPlayer);

        // Player Controller
        pc.player = newPlayer;
        pc.name = newPlayer.name;
        pc.transform.SetParent(playerContainer, false);

        playersPool.Add(newPlayer, playerPool);
    }


    // /!\ Need to add OnPlayerLeft Event /!\ 


    private PlayerPoolUI CreatePlayerPool(GameObject pool, Player player)
    {

        pool.name = player.name + "Pool";
        TextMeshProUGUI tmp = pool.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        tmp.text = player.name;
        tmp.color = player.color;
        TextMeshProUGUI readyText = pool.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        readyText.text = "Not Ready";
        readyText.color = Color.red;

        return  new PlayerPoolUI(pool);
    }


    public void OnCharacterAdded(Player player,Character character)
    {
        playersPool.TryGetValue(player, out PlayerPoolUI pool);
        Transform charUI = pool.characterPool.transform.GetChild(player.characters.Count);
        charUI.GetComponentInChildren<TextMeshProUGUI>().text = character.name;
        charUI.GetComponent<Image>().color = player.color;

        if (player.characters.Count == gameManager.gameMode.CharPerPlayer - 1) {
            pool.state = PlayerPoolUI.PlayerState.CanBeReady;
            pool.UpdateReadyText();
        }

    }

    public void OnCharacterDeleted(Player player)
    {
        playersPool.TryGetValue(player, out PlayerPoolUI pool);
        InitCharPlaceHolder(pool.characterPool.transform.GetChild(player.characters.Count - 1).gameObject);

        pool.state = PlayerPoolUI.PlayerState.Unset;
        pool.UpdateReadyText();
    }

    public void OnReady(Player player){
        playersPool.TryGetValue(player,out PlayerPoolUI pool);
        switch (pool.state)
        {
            case PlayerPoolUI.PlayerState.CanBeReady:
                pool.state = PlayerPoolUI.PlayerState.Ready;
                break;
            case PlayerPoolUI.PlayerState.Ready:
                pool.state = PlayerPoolUI.PlayerState.CanBeReady;
                break;
            case PlayerPoolUI.PlayerState.Unset:
                return;
        }

        pool.UpdateReadyText();

        if (IsEveryPlayerReady() && currentNbOfPlayers == nbOfPlayersRequired)
        {
            SceneManager.LoadScene(gameManager.gameMode.Scene);
        };

    }

    private bool IsEveryPlayerReady(){
        List<PlayerPoolUI> allPlayerPools = new List<PlayerPoolUI>(playersPool.Values);
        foreach (PlayerPoolUI pool in allPlayerPools)
        {
            if (pool.state != PlayerPoolUI.PlayerState.Ready) return false;
        }
        return true;    
    }




    private class PlayerPoolUI
    {
        public GameObject playerPool;

        public GameObject characterPool;

        public TextMeshProUGUI playerName;

        public TextMeshProUGUI ready;

        public TextMeshProUGUI pressStart;

        public enum PlayerState { Unset, CanBeReady, Ready} 

        public PlayerState state;


        public PlayerPoolUI (GameObject playerPool)
        {
            this.playerPool = playerPool;
            playerName = playerPool.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            characterPool = playerPool.transform.GetChild(1).gameObject;
            ready = playerPool.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            pressStart = playerPool.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            state = PlayerState.Unset;
        }


        public void UpdateReadyText() { 
        
            switch (state)
            {
                case PlayerState.Unset:
                    ready.text = "Not Ready";
                    ready.color = Color.red;
                    pressStart.text = "";
                    break;
                case PlayerState.CanBeReady:
                    ready.text = "Not Ready";
                    ready.color = Color.red;
                    pressStart.text = "press Start";
                    break;
                case PlayerState.Ready:
                    ready.text = "Ready";
                    ready.color= Color.green;
                    pressStart.text = "";
                    break;
            }
        
        }




    }

}
