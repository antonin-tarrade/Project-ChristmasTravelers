using Items;
using UnityEngine;

[CreateAssetMenu(fileName = "FlagItem", menuName = "Scriptables/Items/Flag")]
public class FlagItem : DroppableItem
{
    protected override void OnUse(Inventory inventory)
    {
        Debug.Log("FlagItem OnUse");
    }
}