using BoardCommands;
using Records;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInput : MonoBehaviour, IRecordable
{
    [SerializeField] private string characterName;
    public float grabRadius;

    public event Action<IBoardCommand> OnCommandRequest;

    public void GrabNearbyObjects()
    {
        
    }
}
