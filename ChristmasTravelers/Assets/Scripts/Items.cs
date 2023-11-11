using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Items
{

    public enum ItemType { Flag }


    /// <summary>
    /// Can be used to display items in UI
    /// </summary>
    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptables/ItemData")]
    public class ItemData : ScriptableObject
    {

    }

    public interface IItem
    {
        ItemType type { get; }
        void Use(Inventory inventory);
    }

    public interface IMapItem : IItem
    {
        enum MapItemState { Free, Grabbed };

        MapItemState mapState { get; }

        GameObject gameObject { get; }

        void Drop();

        void Initialise();
    }

    public struct MapItemInitialData
    {
        public IMapItem.MapItemState initialState;
        public Vector2 initialPosition;
    }
}
