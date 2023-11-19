using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;



[CreateAssetMenu(fileName = "Flag", menuName = "Items/Flag")]
public class FlagComponent : ItemComponent
{
    public Flag flag { get => (Flag) Item; set => Item = value; }

    public override IItem CreateItem()
    {
        GameObject flagObject = new GameObject("Flag");
        return flagObject.AddComponent<Flag>();
    }
}