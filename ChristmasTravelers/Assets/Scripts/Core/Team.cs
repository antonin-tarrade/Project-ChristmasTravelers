using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "Team", menuName = "Scriptables/Team")]
public class Team : ScriptableObject
{
    public string teamName;
    public Color teamColor;
    public enum Characters {Sniper,Minigun,Rocket};

    [SerializedDictionary("Type of Character", "Sprite Libraries")]
    public SerializedDictionary<Characters, CharacterLibrary> libraries;

}

