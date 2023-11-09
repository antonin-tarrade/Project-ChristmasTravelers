using BoardCommands;
using Records;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class MovementRecorder : MonoBehaviour, IRecorder<MoveBoardCommand>
{
    private double lastUpdateTime;
    private double time;
    private double beginTime;
    private bool isRecording;


    [SerializeField] private RecordData recordData;
    /// <summary>
    /// The recordable object to record
    /// </summary>
    private IRecordable<MoveBoardCommand> recordable;
    private List<TimedBoardCommand> commandList;

    private void Awake()
    {
        commandList = new List<TimedBoardCommand>();
        isRecording = false;
    }

    private void Start()
    {
        recordable = GetComponent<IRecordable<MoveBoardCommand>>();
        recordable.OnCommandRequest += OnRecord;
    }

    public void BeginRecord()
    {
        commandList = new List<TimedBoardCommand>();
        isRecording = true;
        beginTime = Time.time;
        time = 0;
        lastUpdateTime = 0;
    }
    public void EndRecord()
    {
        isRecording = false;
    }

    public void SaveRecord(Replay replay)
    {
        foreach (TimedBoardCommand command in commandList)
        {
            replay.Add(command);
        }
    }

    /// <summary>
    /// Called to record the current command
    /// </summary>
    public void OnRecord(MoveBoardCommand command)
    {
        if (!isRecording) return;

        time = Time.time - beginTime;

        if ((time - lastUpdateTime) > recordData.recordFrequency)
        {
            command.movement = ((float)recordData.recordFrequency) * command.movement;
            commandList.Add(new TimedBoardCommand(time, command));
            BoardManager.instance.Execute(command);
            lastUpdateTime = time;
        }
    }
}
