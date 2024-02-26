using BoardCommands;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.InputSystem.InputAction;

public class CharacterInput : SimpleInput
{
    private InputAction aimAction;
    private Character character;
    private IAttack attack;
    private Inventory inventory;

    private void Awake()
    {
        character = GetComponent<Character>();
        inventory = GetComponent<Inventory>();
        attack = GetComponent<IAttack>();
        isActive = true;
    }

    public void Set(PlayerInput playerInput)
    {
        aimAction = playerInput.actions["ShootDirection"];
    }

    public void Grab(CallbackContext context)
    {
        if (context.started) RequestCommand(new GrabCommand(character));
    }


    private bool shoot;
    public void Shoot(CallbackContext context)
    {
        if (context.started || context.performed) shoot = true;
        else shoot = false;
    }


    public void Shoot()
    {
        RequestCommand(attack.GenerateShootCommand());
    }

    public void UpdateShootDirection(Vector3 direction)
    {
        RequestCommand(attack.GenerateAimCommand(direction));
    }


    public void UseItem(CallbackContext context)
    {
        if (context.started)
        {
            IItem item = inventory.GetCurrentItem();
            if (item != null) RequestCommand(item.GenerateCommand(character));
        }
    }

    public void NextItem(CallbackContext context)
    {
        if (context.started) inventory.NextItem();
    }

    public void PreviousItem(CallbackContext context)
    {
        if (context.started) inventory.PreviousItem();
    }

    private void Update()
    {
        UpdateShootDirection(aimAction.ReadValue<Vector2>());
        if (shoot) Shoot();
    }
}
