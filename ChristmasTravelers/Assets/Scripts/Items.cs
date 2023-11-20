using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Items
{

    public interface IGrabbable 
    {
        void AcceptCollect(Character character);
    }

    /// <summary>
    /// Can be used to display items in UI
    /// </summary>
    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptables/ItemData")]
    public class ItemData : ScriptableObject
    {
        
    }

    public interface IItem 
    {
        void Use(Inventory inventory);

        void RegisterInitialState(Inventory inventory);
    }
    


    public abstract class ScriptableItem : ScriptableObject, IItem
    {
        public abstract void Use(Inventory inventory);

        public virtual void RegisterInitialState(Inventory inventory)
        {
            ItemManager.instance.Register(new ScriptableItemInitialData(this, inventory));
        }
    }


    public abstract class ConsumableItem : ScriptableItem{

        public override void Use(Inventory inventory)
        {
            inventory.Remove(this);
            OnUse(inventory);
        }

        protected abstract void OnUse(Inventory inventory);
    }


    public abstract class DroppableItem : ConsumableItem{


        protected void Drop(Vector3 position)
        {
            GameObject container = new GameObject();
            container.transform.position = position;
            GrabbableItem grabbableItem = container.AddComponent<GrabbableItem>();
            grabbableItem.Set(this);
        }

        public void RegisterInitialState(GrabbableItem grabbable)
        {
            ItemManager.instance.Register(new DroppableItemInitialData(grabbable, null, this, grabbable.transform.position));
        }

        public override void RegisterInitialState(Inventory inventory)
        {
            ItemManager.instance.Register(new DroppableItemInitialData(null, inventory, this, Vector3.zero));
        }

    }

    public interface IItemInitialData {
        void RestoreInitialState();
    }

    public struct ScriptableItemInitialData : IItemInitialData {
        public ScriptableItem item;
        public Inventory inventory;

        public ScriptableItemInitialData(ScriptableItem item, Inventory inventory){
            this.item = item;
            this.inventory = inventory;
        }

        public void RestoreInitialState(){
            ItemManager.instance.RestoreInitialState(this);
        }
    }

    public struct DroppableItemInitialData : IItemInitialData{
        public GrabbableItem grabbable;
        public Inventory inventory;
        public DroppableItem item;
        public Vector3 initialPosition;

        public DroppableItemInitialData(GrabbableItem grabbable, Inventory inventory, DroppableItem item, Vector3 initialPosition){
            this.grabbable = grabbable;
            this.inventory = inventory;
            this.item = item;
            this.initialPosition = initialPosition;
        }

        public void RestoreInitialState() {
            ItemManager.instance.RestoreInitialState(this);
        }
    }
    

}
