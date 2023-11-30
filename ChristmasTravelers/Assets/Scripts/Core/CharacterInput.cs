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
        shootDirection = Vector3.zero;
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
    private Vector3 shootDirection;
    public void Shoot(CallbackContext context)
    {
        action = context.action;
        if (context.started || context.performed) shoot = true;
        else shoot = false;
    }

    public void Shoot()
    {
        if (!attack.IsCooldownReady()) return;
        InputAction directionAction = action.actionMap.FindAction("ShootDirection");
        Vector2 shootDirectionInput = directionAction.ReadValue<Vector2>();
        if (shootDirectionInput.sqrMagnitude != 0) shootDirection = shootDirectionInput;
        if (shootDirection.sqrMagnitude == 0) return;
        RequestCommand(attack.GenerateCommand(shootDirection));
    }


    private void Update()
    {
        if (shoot) Shoot();
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
}
