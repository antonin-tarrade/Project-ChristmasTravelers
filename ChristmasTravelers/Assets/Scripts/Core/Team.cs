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
    public SpriteLibraryAsset spriteLibrary;
    public SpriteLibraryAsset chikenSpriteLibrary;
    public Color teamColor;

    private void OnValidate() {
        teamName = Path.GetFileNameWithoutExtension( AssetDatabase.GetAssetPath(this));
    }
}
