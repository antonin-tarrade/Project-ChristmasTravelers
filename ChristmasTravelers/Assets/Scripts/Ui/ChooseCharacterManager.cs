using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCharacterManager : MonoBehaviour
{

    public static ChooseCharacterManager instance;

    private GameObject[] allCharacters;


    [SerializeField] Transform canvas;
    [SerializeField] GameObject characterUiMini;
    [SerializeField] GameObject characterUiBig;

    private Transform allCharactersPool;
    private Transform player1Pool;
    private Transform player2Pool;



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
        allCharacters = Resources.LoadAll<GameObject>("Characters");
        allCharactersPool = canvas.Find("AllCharactersPool");
        player1Pool = canvas.Find("Player1Pool");
        player2Pool = canvas.Find("Player2Pool");

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
            characterUi.transform.parent = allCharactersPool;
            characterUi.GetComponent<Button>().onClick.AddListener(() => SelectCharacter(ch));
        }

    }

    private void SelectCharacter(GameObject character){
        GameObject characterUi = Instantiate(characterUiBig,player1Pool.transform);
        characterUi.GetComponentInChildren<TextMeshProUGUI>().text = character.name;
        characterUi.transform.parent = player1Pool;

    }

}
