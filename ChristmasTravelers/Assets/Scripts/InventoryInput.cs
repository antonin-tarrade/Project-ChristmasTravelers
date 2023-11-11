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
    private Inventory inventory;


    // TO DO : A mettre dans le character
    public float grabRadius;

    public void Grab(CallbackContext context)
    {
        if (context.started) RequestCommand(new GrabCommand(gameObject));
    }
}
