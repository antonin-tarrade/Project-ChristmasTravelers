using BoardCommands;
using Records;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class MovementRecorder : SimpleRecorder
{
    private double lastUpdateTime;

    /// <summary>
    /// The recordable object to record
    /// </summary>
    [SerializeField] private MovementInput recordable;

    private new void Start()
    {
        recordable.OnCommandRequest += OnRecord;
    }

    public override void BeginRecord()
    {
        base.BeginRecord();
        lastUpdateTime = 0;
    }

    /// <summary>
    /// Called to record the current command
    /// </summary>
    protected override void OnRecord(IBoardCommand command)
    {
        MoveBoardCommand moveCommand = (MoveBoardCommand) command;
        if (!isRecording) return;

        time = Time.time - beginTime;

        if ((time - lastUpdateTime) > recordData.recordFrequency)
        {
            moveCommand.movement = ((float)recordData.recordFrequency) * moveCommand.movement;
            commandList.Add(new TimedBoardCommand(time, moveCommand));
            moveCommand.Execute();
            lastUpdateTime = time;
        }
    }
}
