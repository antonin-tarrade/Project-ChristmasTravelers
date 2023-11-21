using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using System;


public class DashItem : Item
{
    protected new DashItemData data;
    public override void OnUse(Inventory inventory)
    {
        Character character = inventory.GetComponent<Character>();
        character.GetComponent<DashComponent>().Dash(data.dashSpeed, data.dashDistance);
        inventory.Remove(this);
    }

    public override string GetName()
    {
        return "Dash";
    }

    public DashItem(DashItemData data) : base(data) 
    {
        this.data = data;
    }
}

[CreateAssetMenu(fileName = "DashItem", menuName = "Scriptables/Items/Dash")]
public class DashItemData : ScriptableItemData
{
    public float dashSpeed;
    public float dashDistance;

    public override IItem GetInstance()
    {
        return new DashItem(this);
    }
}

