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

    public interface IItem 
    {
        string GetName();
        void Use(Inventory inventory, IItemParameters parameters);
        UseItemCommand GenerateCommand(Character character);
        void RegisterInitialState(ItemManager itemManager, Inventory inventory);
        void RegisterInitialState(ItemManager itemManager, GrabbableItem grabbable);
    }

    public abstract class Item : IItem
    {
        protected ScriptableItemData data;
        public abstract string GetName();
        public void Use(Inventory inventory, IItemParameters parameters)
        {
            if (inventory.Contains(this))
            {
                if (data.consumeOnUse) inventory.Remove(this);
                OnUse(inventory, parameters);
            }
        }

        public abstract void OnUse(Inventory inventory, IItemParameters parameters);

        public abstract UseItemCommand GenerateCommand(Character character);

        public void Drop(Inventory inventory) { }

        public void RegisterInitialState(ItemManager itemManager, GrabbableItem grabbable)
        {
            itemManager.Register(new GrabbableItemInitialData(grabbable, this, grabbable.transform.position));
        }

        public void RegisterInitialState(ItemManager itemManager, Inventory inventory)
        {
            itemManager.Register(new InventoryItemInitialData(inventory, this));
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

    public interface IItemInitialData
    {
        public void RestoreInitialState(ItemManager itemManager);
    }

    public struct InventoryItemInitialData : IItemInitialData
    {
        public Inventory inventory;
        public IItem item;

        public InventoryItemInitialData(Inventory inventory, Item item)
        {
            this.inventory = inventory;
            this.item = item;
        }

        public void RestoreInitialState(ItemManager itemManager)
        {
            itemManager.RestoreInitialState(this);
        }
    }

    public struct GrabbableItemInitialData : IItemInitialData {
        public GrabbableItem grabbable;
        public IItem item;
        public Vector3 initialPosition;

        public GrabbableItemInitialData(GrabbableItem grabbable, IItem item, Vector3 initialPosition){
            this.grabbable = grabbable;
            this.item = item;
            this.initialPosition = initialPosition;
        }

        public void RestoreInitialState(ItemManager itemManager) {
            itemManager.RestoreInitialState(this);
        }
    }

    public interface IItemParameters
    {


    }

    public struct DashItemParameters : IItemParameters
    {
        public Vector3 direction;

        public DashItemParameters(Vector3 direction)
        {
            this.direction = direction;
        } 
    }
   
}
