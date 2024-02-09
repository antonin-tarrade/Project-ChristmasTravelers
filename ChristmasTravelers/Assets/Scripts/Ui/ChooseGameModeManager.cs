using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseGameModeManager : MonoBehaviour
{

    [SerializeField] private RectTransform gameModePool;
    [SerializeField] private Button button;
    private GameModeData[] allModes;


    // Start is called before the first frame update
    void Start()
    {
        allModes = Resources.LoadAll<GameModeData>("GameModes").Where(mode => !mode.name.StartsWith('[')).ToArray();
        InitGameModePool();

    }


    private void InitGameModePool()
    {
        foreach (GameModeData mode in allModes)
        {
            AddButtonFor(mode);
        }
        gameModePool.GetChild(0).gameObject.GetComponent<Button>().Select();
    }

    public void AddButtonFor(GameModeData gameModeData)
    {
        Button buttonInstance = Instantiate(button, gameModePool);
        buttonInstance.GetComponentInChildren<TextMeshProUGUI>().text = gameModeData.gameModeName;
        buttonInstance.onClick.AddListener(() => gameModeData.Select());
    } 


    public void ChooseCharacter(){
        GameModeData.selectedMode.players.Clear();
        SceneManager.LoadScene("ChooseCharacter");
    }
}
