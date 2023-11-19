using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public event Action<IItem> OnItemAdded;
    public event Action<IItem> OnItemRemoved;
    public event Action<IItem> OnItemChanged;

    
    [SerializeReference] List<ItemComponent> items;

    public IItem currentItem { get; private set; }
    private int currentItemIndex;


    private void Awake()
    {
        // items = new List<ItemComponent>();
        currentItemIndex = 0;
        currentItem = (items.Count == 0)? null : items[0].Item;
    }

    public void Add(IItem item)
    {
        items.Add(new ItemComponent() {Item = item });
        OnItemAdded?.Invoke(item);
    }

    public void Remove(IItem item)
    {
        items.Remove(items.Find(x => x.Item == item));
        Debug.Log(items.Count);
        OnItemRemoved?.Invoke(item);
    }

    public void NextItem()
    {
        if (items.Count == 0) return;
        if (currentItem == null)
        {
            currentItem = items[0].Item;
            return;
        }
        currentItemIndex++;
        if (currentItemIndex >= items.Count) currentItemIndex = 0;
        currentItem = items[currentItemIndex].Item;
        OnItemChanged?.Invoke(currentItem);
    }

    public void PreviousItem()
    {
        if (items.Count == 0) return;
        if (currentItem == null)
        {
            currentItem = items[0].Item;
            return;
        }
        currentItemIndex--;
        if (currentItemIndex < 0) currentItemIndex = items.Count - 1;
        currentItem = items[currentItemIndex].Item;
        OnItemChanged?.Invoke(currentItem);
    }


}
