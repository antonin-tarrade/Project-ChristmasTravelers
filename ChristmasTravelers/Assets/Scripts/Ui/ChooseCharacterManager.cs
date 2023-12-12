using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;
using UnityEngine.InputSystem.UI;
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

    private Dictionary<Player, GameObject> playersPool;

    [SerializeField] private int nbOfPlayers;
    [SerializeField] private int maxColumns;

    private Color[] couleurs; //debug

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
        couleurs = new Color[]{ Color.blue, Color.red };
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

        int nbRow = allCharacters.Length / maxColumns;

        matrice = new CharacterComponent[nbRow][];
        for (int i = 0; i < nbRow ; i++){
            matrice[i] = new CharacterComponent[maxColumns];
        }

        int currentCol = 0;
        int currentRow = 0;
        foreach (GameObject ch in allCharacters) {
            GameObject characterUi = Instantiate(characterUiMini,allCharactersPool.transform);
            CharacterComponent component = characterUi.GetComponent<CharacterComponent>();
            characterUi.GetComponentInChildren<TextMeshProUGUI>().text = ch.name;
            characterUi.transform.SetParent(allCharactersPool);
            if (currentCol > maxColumns){
                currentCol = 0;
                currentRow ++;
            } 
            component.charPrefab = ch;
            component.position = new Tuple<int, int>(currentRow,currentCol);
            matrice[currentRow][currentCol] = component;
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

        playersPool.Add(newPlayer, poolGO);
    }

}
