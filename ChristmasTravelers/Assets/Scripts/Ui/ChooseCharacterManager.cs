using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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
    [SerializeField] private Transform playerContainer;

    private Transform allCharactersPool;
    private Transform allPlayersPool;

    private Dictionary<Player, GameObject> playersPool;

    [SerializeField] private int nbOfPlayers;
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
        
        playersPool = new Dictionary<Player, GameObject>();



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
            component.position = new Tuple<int, int>(currentRow,currentCol);
            currentCol++;
        }

        int total = 0;
        for (int i = 0;i < nbRow ; i++)
        {
            for (int j = 0; j < maxColumns && total < allCharacters.Length; j++)
            {  
                matrice[i][j].right = (j + 1 == maxColumns || matrice[i][j+1] == null) ? matrice[i][0] : matrice[i][j + 1];
                if (j == 0) {
                    int last = nbRow -1 ;
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
            GameObject pool = Instantiate(playerPool,allPlayersPool.transform);
            pool.name = "UnsetPool";
            pool.transform.SetParent(allPlayersPool);
        }
    }


    public void OnPlayerJoined (){

        PlayerController pc = GameObject.Find("PlayerController(Clone)").GetComponent<PlayerController>();

        Player newPlayer = new Player
        {
            name = "Player" + gameManager.players.Count,
            color = couleurs[gameManager.players.Count]
            
        };

        gameManager.players.Add(newPlayer);
        Transform pool = allPlayersPool.Find("UnsetPool");
        GameObject poolGO = pool.gameObject;
        poolGO.name = newPlayer.name + "Pool";
        poolGO.GetComponent<TextMeshProUGUI>().text = newPlayer.name;

        pc.player = newPlayer;
        pc. name = newPlayer.name;

        pc.transform.parent = playerContainer;

        playersPool.Add(newPlayer, poolGO);
    }

}
