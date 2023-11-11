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
        public void Use(Inventory inventory);
        public void Drop();
    }
}
