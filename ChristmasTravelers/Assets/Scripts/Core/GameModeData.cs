using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameModeData", menuName = "Scriptables/GameModeData")]
public class GameModeData : ScriptableObject
{
    public string gameModeName;
    public SceneAsset scene;
    public int nbOfPlayers;
    public int charPerPlayer;
    public Vector3[] spawns;
    public float roundDuration;


    private void OnValidate()
    {
        if (spawns.Length != nbOfPlayers)
        {
            spawns = new Vector3[nbOfPlayers];
        }
    }
}
