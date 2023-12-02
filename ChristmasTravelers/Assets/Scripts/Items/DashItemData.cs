using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using System;
using BoardCommands;


public class DashItem : Item
{
    protected new DashItemData data;
    public override void OnUse(Inventory inventory, IItemParameters parameters)
    {
        Character character = inventory.GetComponent<Character>();
        Vector3 direction = ((DashItemParameters)parameters).direction;
        character.GetComponent<DashComponent>().Dash(data.dashSpeed, data.dashDistance, direction);
    }

    public override UseItemCommand GenerateCommand(Character character)
    {
        Vector3 direction = character.GetComponent<MovementInput>().GetMovementDirection();
        return new UseItemCommand(character, this, new DashItemParameters(direction));
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

public struct DashItemParameters : IItemParameters
{
    public Vector3 direction;

    public DashItemParameters(Vector3 direction)
    {
        this.direction = direction;
    }
}

