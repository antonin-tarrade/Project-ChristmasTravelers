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
    private InputAction action;
    private Character character;
    private IAttack attack;
    private Inventory inventory;

    private void Awake()
    {
        rightStickDirection = Vector3.up;
    }

    private void Start()
    {
        character = GetComponent<Character>();
        inventory = GetComponent<Inventory>();
        attack = GetComponent<IAttack>();
    }

    public void Grab(CallbackContext context)
    {
        if (context.started) RequestCommand(new GrabCommand(character));
    }


    private bool shoot;
    public Vector3 rightStickDirection { get; private set; }
    public void Shoot(CallbackContext context)
    {
        action = context.action;
        if (context.started || context.performed) shoot = true;
        else shoot = false;
    }


    public void Shoot()
    {
        RequestCommand(attack.GenerateShootCommand());
    }

    public void UpdateShootDirection(Vector3 direction)
    {
        //rightStickDirection = (direction.sqrMagnitude == 0) ? rightStickDirection : direction;
        if (direction.sqrMagnitude > 0) RequestCommand(attack.GenerateAimCommand(direction));
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



    private void Update()
    {
        if (action != null)
        {
            UpdateShootDirection(action.actionMap.FindAction("ShootDirection").ReadValue<Vector2>());
        }
        if (shoot) Shoot();
    }
}
