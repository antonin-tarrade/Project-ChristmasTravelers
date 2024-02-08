using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameModeData", menuName = "Scriptables/GameModeData")]
public class GameModeData : ScriptableObject
{
    public static GameModeData selectedMode;
    public string gameModeName;
#if UNITY_EDITOR
    public SceneAsset scene;
#endif
    public string sceneName;
    public int nbOfPlayers;
    public int charPerPlayer;
    public Vector3[] spawns;
    public float roundDuration;
    public List<Player> players;

    public bool selected = false;


    private void OnValidate()
    {
        if (spawns.Length != nbOfPlayers)
        {
            spawns = new Vector3[nbOfPlayers];
        }
        if (selected)
        {
            if (selectedMode && selectedMode != this) selectedMode.selected = false;
            selectedMode = this;
        }
#if UNITY_EDITOR
        sceneName = scene.name;
#endif
    }
}
