using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IPreparable, IItemContainer
{

    // public event Action<ScriptableItem> OnItemAdded;
    // public event Action<ScriptableItem> OnItemRemoved;
    // public event Action<ScriptableItem> OnItemChanged;


    [SerializeField] List<ScriptableItemData> itemsData;
    public List<IItem> items { get; private set; }
    private List<IPreparable> initialData;
    //DEBUG
    public List<string> itemsNames;
    public string itemSelected;
    //FIN 

    private int currentItemIndex;


    private void Awake()
    {
        currentItemIndex = 0;
        items = new List<IItem>();
        itemsNames = new List<string>();
        initialData = new List<IPreparable>();
        foreach (ScriptableItemData data in  itemsData)
        {
            items.Add(data.GetInstance());
        }
        foreach (IItem item in items)
        {
            initialData.Add(new InventoryItemInitialData(this, item));
        }
    }

    private void Start() {
        GameManager.instance.Register(this);
    }

    private void Update()
    {
        // Debug degueulasse
        itemSelected = GetCurrentItem()?.GetName();
        if (items.Count != itemsNames.Count)
        {
            itemsNames.Clear();
            foreach (IItem item in items)
            {
                itemsNames.Add(item.GetName());
            }
        }
        // FIN
    }

    public void Prepare()
    {
        items.Clear();
        foreach (IPreparable p in initialData)
        {
            p.Prepare();
        }
    }

    public IItem GetCurrentItem() {
        if (items.Count == 0) return null;
        return items[currentItemIndex];
    }

    public void Add(IItem item)
    {
        items.Add(item);
    }

    public void Remove(IItem item)
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

    public bool Contains(IItem item)
    {
        return items.Contains(item);
    }

    public void Clear() {
        items.Clear();
    }


}

public struct InventoryItemInitialData : IPreparable
{
    public Inventory inventory;
    public IItem item;

    public InventoryItemInitialData(Inventory inventory, IItem item)
    {
        this.inventory = inventory;
        this.item = item;
    }

    public void Prepare()
    {
        inventory.Add(item);
    }
}
