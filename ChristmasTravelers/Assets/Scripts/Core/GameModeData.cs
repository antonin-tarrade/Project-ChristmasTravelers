using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.U2D.Animation;

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
    public float roundDuration;
    public List<Player> players;

    public enum Teams{Human,Alien};

    public UDictionary<Teams,SpriteLibraryAsset> teams;

    public void Select()
    {
        if (selectedMode && selectedMode != this) selectedMode.selected = false;
        selectedMode = this;
        selected = true;
    }

    [Header("DEBUG")]
    public bool selected = false;
    public bool overrideControllers;
    public bool uniqueController;
    public int uniqueControllerIndex;
    public List<string> allowedDevices;



    private void OnValidate()
    {
        if (selected)
        {
            Select();
        }
#if UNITY_EDITOR
        sceneName = scene.name;
#endif
        nbOfPlayers = players.Count;
    }

}


public static class GameModeDataUtility
{
    /// <summary>
    /// Returns all the colliders of the active characters of the currently selected game mode
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Collider2D> AllCharactersColliders()
        => GameModeData.selectedMode.AllCharactersColliders();

    /// <summary>
    /// Returns all the colliders of the active characters of a given game mode
    /// </summary>
    /// <param name="data">The game mode data</param>
    /// <returns></returns>
    public static IEnumerable<Collider2D> AllCharactersColliders(this GameModeData data)
        => data.players.SelectMany(p => p.characterInstances.Select(c => c.GetComponent<Collider2D>()));
}
