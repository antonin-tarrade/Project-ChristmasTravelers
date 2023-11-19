using BoardCommands;
using Records;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using System.Linq;
using static UnityEngine.InputSystem.InputAction;

public class InventoryInput : SimpleInput
{

    private Character character;
    private Inventory inventory;


    private void Start()
    {
        character = GetComponent<Character>();
        inventory = GetComponent<Inventory>();
    }

    public void Grab(CallbackContext context)
    {
        if (context.started) RequestCommand(new GrabCommand(character));
    }

    public void UseItem(CallbackContext context)
    {
        if (context.started) RequestCommand(new UseItemCommand(character, inventory.currentItem));
    }

    public void NextItem(CallbackContext context)
    {
        if (context.started) inventory.NextItem();
    }

    public void PreviousItem(CallbackContext context)
    {
        if (context.started) inventory.PreviousItem();
    }
}
