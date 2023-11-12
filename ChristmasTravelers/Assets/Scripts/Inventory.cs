using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public event Action<IItem> OnItemAdded;
    public event Action<IItem> OnItemRemoved;

    private List<IItem> items;

    private void Awake()
    {
        items = new List<IItem>();
    }

    public void Add(IItem item)
    {
        items.Add(item);
        OnItemAdded?.Invoke(item);
    }

    public void Remove(IItem item)
    {
        items.Remove(item);
        OnItemRemoved?.Invoke(item);
    }
}
