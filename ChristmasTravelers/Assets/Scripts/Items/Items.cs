using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BoardCommands;
using UnityEngine.TextCore.Text;

namespace Items
{

    public interface IGrabbable 
    {
        void AcceptCollect(Character character);
    }

    public interface IItemParameters { }

    public interface IItem 
    {
        Action<IItem> OnUseEvent { get; set; }
        Action<IItem> OnDropEvent { get; set; }
        IItemContainer container { get; set; }
        string GetName();
        void Use(Inventory inventory, IItemParameters parameters);
        void Drop();
        UseItemCommand GenerateCommand(Character character);
    }

    public interface IItemContainer
    {
        GameObject gameObject { get;}
        void Add(IItem item);
        void Remove(IItem item);
        bool Contains(IItem item);
    }

    public abstract class Item : IItem
    {
        public Action<IItem> OnUseEvent { get; set; }
        public Action<IItem> OnDropEvent { get; set; }
        public IItemContainer container { get; set; }

        protected ScriptableItemData data;
        public abstract string GetName();
        public void Use(Inventory inventory, IItemParameters parameters)
        {
            if (inventory.Contains(this))
            {
                if (data.consumeOnUse) inventory.Remove(this);
                OnUseEvent?.Invoke(this);
                OnUse(inventory, parameters);
            }
        }

        public abstract void OnUse(Inventory inventory, IItemParameters parameters);

        public abstract UseItemCommand GenerateCommand(Character character);

        public void Drop()
        {
            container.Remove(this);
            OnDropEvent?.Invoke(this);         
        }

        public Item(ScriptableItemData data)
        {
            this.data = data;
        }

    }

    public abstract class ScriptableItemData : ScriptableObject
    {
        public abstract IItem GetInstance();
        public bool consumeOnUse;
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

    public struct GrabbableItemInitialData : IPreparable {
        public GrabbableItem grabbable;
        public IItem item;
        public Vector3 initialPosition;

        public GrabbableItemInitialData(GrabbableItem grabbable, IItem item, Vector3 initialPosition){
            this.grabbable = grabbable;
            this.item = item;
            this.initialPosition = initialPosition;
        }

        public void Prepare() {
            grabbable.gameObject.SetActive(true);
            grabbable.transform.position = initialPosition;
            grabbable.Set(item);
        }
    }

    
   
}
