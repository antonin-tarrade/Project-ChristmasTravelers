using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Records;
using BoardCommands;
using System;

public class MovementInput : MonoBehaviour, IRecordable<MoveBoardCommand>
{
    public event Action<MoveBoardCommand> OnCommandRequest;

    private Vector2 movement;

    [SerializeField] private float speed;

    private void Awake()
    {
        movement = Vector2.zero;
    }

    private void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (movement.magnitude > 0) OnCommandRequest?.Invoke(new MoveBoardCommand(gameObject, speed * movement));
    }
}
