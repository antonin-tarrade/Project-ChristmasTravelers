using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "Scriptables/GameData")]
public class GameData : ScriptableObject
{
    public Material LivingMaterial;
    public Material DeadMaterial;
    public Material GhostMaterial;
}
