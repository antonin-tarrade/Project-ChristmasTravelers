using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private List<IItem> items;

    private void Awake()
    {
        items = new List<IItem>();
    }

    public void Add(IItem item)
    {
        items.Add(item);
    }

    public void Remove(IItem item)
    {
        items.Remove(item);
    }
}
