using BoardCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CharacterInput : SimpleInput
{
    private Character character;
    private CharacterAttack attack;

    private void Start()
    {
        character = GetComponent<Character>();
        attack = GetComponent<CharacterAttack>();
    }

    public void Grab(CallbackContext context)
    {
        if (context.started) RequestCommand(new GrabCommand(character));
    }

    public void Shoot(CallbackContext context)
    {
        InputAction directionAction = context.action.actionMap.FindAction("ShootDirection");
        Vector2 shootDirection = directionAction.ReadValue<Vector2>();
        if (shootDirection.sqrMagnitude == 0) return;
        if (context.started) RequestCommand(new ShootCommand(attack, shootDirection));
    }
}
