using BoardCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (context.started) RequestCommand(new ShootCommand(attack, Vector3.right));
    }
}
