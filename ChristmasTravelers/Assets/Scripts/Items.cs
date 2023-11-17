using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Items
{


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
    }
    public struct MapItemInitialData
    {
        public IMapItem.MapItemState initialState;
        public Vector2 initialPosition;
    }

    public abstract class IMapItem : MonoBehaviour, IItem
    {

        public enum MapItemState { Free, Grabbed };

        public MapItemState mapState { get; protected set;}

        public Character characterHoldingThis { get; protected set; }

        public void Use(Inventory inventory)
        {
            if (mapState == MapItemState.Grabbed) Drop();
        }

        public virtual void Drop() {
            transform.position = characterHoldingThis.transform.position;  
            characterHoldingThis.GetComponent<Inventory>().Remove(this);
            mapState = IMapItem.MapItemState.Free;
            characterHoldingThis = null;
        }

        public abstract void Initialise();
    }


    public abstract class ItemConsumable : IItem {

        public void Use(Inventory inventory)
        {
            inventory.Remove(this);
            OnUse(inventory.GetComponent<Character>());
        }

        protected abstract void OnUse(Character character);
    }

    public class DashItem : ItemConsumable {

        private string name;
        private ItemData data;

        override protected void OnUse(Character character){
            character.StartCoroutine(character.GetComponent<Dash>().ApplyDash(character));
        }

    }

}
