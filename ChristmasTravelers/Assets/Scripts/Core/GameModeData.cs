using System;
using System.Collections;
using System.Collections.Generic;
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
    public List<string> allowedDevices;



    private void OnValidate()
    {
        if (selected)
        {
            Select();
            foreach (Player player in players){
                SelectSkin(player);
            }
        }
#if UNITY_EDITOR
        sceneName = scene.name;
#endif
    }

    private void SelectSkin(Player player){
        foreach (Character character in player.characterPrefabs){
            teams.TryGetValue(player.team, out SpriteLibraryAsset skin);
            character.GetComponent<SpriteLibrary>().spriteLibraryAsset = skin;
        }
    }
}
