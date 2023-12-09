using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    private List<IItemInitialData> initialData;
    private List<Inventory> inventories;

    public void Awake()
    {
        instance = this;
        initialData = new List<IItemInitialData>();
        inventories = new List<Inventory>();
    }

    private void Start()
    {
        GameManager.instance.OnTurnStart += OnTurnStart;
        GameManager.instance.OnTurnEnd += OnTurnEnd;
    }

    public void Register(IItemInitialData data)
    {
        initialData.Add(data);
    }

    public void Register(Inventory inventory)
    {
        inventories.Add(inventory);
    }


    private void RestoreInitialState()
    {
        //foreach (Inventory inventory in inventories) {
        //    inventory.Clear();
        //}
        //foreach (IItemInitialData data in initialData) {
        //    data.RestoreInitialState();
        //}
    }


    private void OnTurnStart()
    {
        RestoreInitialState();
    }

    private void OnTurnEnd()
    {

    }

    
}
