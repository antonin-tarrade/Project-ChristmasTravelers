using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameModeData", menuName = "Scriptables/GameModeData")]
public class GameModeData : ScriptableObject
{
    public static GameModeData selectedMode { get; private set; }
    public string gameModeName;
#if UNITY_EDITOR
    public SceneAsset scene;
#endif
    public string sceneName;
    public int nbOfPlayers;
    public int charPerPlayer;
    public int roundsNumber;
    public Vector3[] spawns;
    public float roundDuration;
    public List<Player> players;

    
    public void Select()
    {
        if (selectedMode && selectedMode != this) selectedMode.selected = false;
        selectedMode = this;
        selected = true;
    }

    [Header("DEBUG")]
    public bool selected = false;
    public bool overrideControllers;
    public List<string> allowedDevices;


    private void OnValidate()
    {
        if (spawns.Length != nbOfPlayers)
        {
            spawns = new Vector3[nbOfPlayers];
        }
        if (selected)
        {
            Select();
        }
#if UNITY_EDITOR
        sceneName = scene.name;
#endif
    }
}
