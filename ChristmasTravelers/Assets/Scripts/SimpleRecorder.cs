using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Records;
using BoardCommands;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class SimpleRecorder : MonoBehaviour, IRecorder
{
    protected double time;
    protected double beginTime;
    protected bool isRecording;
    [SerializeField] protected RecordData recordData;
    [SerializeReference] protected List<SimpleInput> recordables;
    protected List<TimedBoardCommand> commandList;


    protected void Awake()
    {
        commandList = new List<TimedBoardCommand>();
        isRecording = false;
    }

    protected void Start()
    {
        foreach (IRecordable recordable in recordables)
        {
            recordable.OnCommandRequest += OnRecord;
        }
    }

    public virtual void BeginRecord()
    {
        commandList = new List<TimedBoardCommand>();
        isRecording = true;
        beginTime = Time.time;
        time = 0;
    }

    public virtual void EndRecord()
    {
        isRecording = false;
    }

    public virtual void SaveRecord(Replay replay)
    {
        foreach (TimedBoardCommand command in commandList)
        {
            replay.Add(command);
        }
    }

    protected virtual void OnRecord(IBoardCommand command)
    {
        if (!isRecording) return;
        time = Time.time - beginTime;
        commandList.Add(new TimedBoardCommand(time, command));
        BoardManager.instance.Execute(command);
    }

    

}
