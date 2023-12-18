using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChooseGameModeManager : MonoBehaviour
{

    [SerializeField] private RectTransform gameModePool;
    private GameObject[] allModes;


    // Start is called before the first frame update
    void Start()
    {
        allModes = Resources.LoadAll<GameObject>("GameModes").Where(mode => !mode.name.StartsWith('[')).ToArray();
        InitGameModePool();

    }


    private void InitGameModePool()
    {
        foreach (GameObject mode in allModes)
        {
          GameObject gameMode = Instantiate(mode, gameModePool);
        }
    }
}
