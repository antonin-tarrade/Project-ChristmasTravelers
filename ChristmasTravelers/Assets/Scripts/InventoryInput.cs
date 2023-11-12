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


    private void Start()
    {
        character = GetComponent<Character>();
    }

    public void Grab(CallbackContext context)
    {
        if (context.started) RequestCommand(new GrabCommand(character));
    }
}
