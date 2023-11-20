using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    // public event Action<ScriptableItem> OnItemAdded;
    // public event Action<ScriptableItem> OnItemRemoved;
    // public event Action<ScriptableItem> OnItemChanged;


    [SerializeField] List<ScriptableItem> items;

    private int currentItemIndex;


    private void Awake()
    {
        // items = new List<ItemComponent>();
        currentItemIndex = 0;
        
    }

    private void Start() {
        ItemManager.instance.Register(this);
        foreach (ScriptableItem item in items) {
            item.RegisterInitialState(this);
        }
    }

    public ScriptableItem GetCurrentItem() {
        if (items.Count == 0) return null;
        return items[currentItemIndex];
    }

    public void Add(ScriptableItem item)
    {
        items.Add(item);
    }

    public void Remove(ScriptableItem item)
    {
        items.Remove(item);
        if (items.Count == 0) currentItemIndex = 0;
        else currentItemIndex = Mathf.Min(currentItemIndex, items.Count - 1);
    }

    public void NextItem()
    {
        if (items.Count == 0) return;
        currentItemIndex++;
        if (currentItemIndex >= items.Count) currentItemIndex = 0;
    }

    public void PreviousItem()
    {
        if (items.Count == 0) return;

        currentItemIndex--;
        if (currentItemIndex < 0) currentItemIndex = items.Count - 1;
    }

    public void Clear() {
        items.Clear();
    }


}
