using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;


[CreateAssetMenu(fileName = "Dash", menuName = "Items/Dash")]
public class DashComponent : ItemComponent
{
    [Header("Dash Settings")]

    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;

    public DashItem dashItem { get => (DashItem) Item; set => Item = value; }

    public override IItem CreateItem()
    {
        return new DashItem(dashSpeed, dashTime);
    }
}




