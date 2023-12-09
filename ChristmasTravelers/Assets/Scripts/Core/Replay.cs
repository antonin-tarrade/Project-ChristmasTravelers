using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardCommands;


/// <summary>
/// Class that can save timed actions and play them 
/// </summary>
public class Replay : MonoBehaviour
{
    /// <summary>
    /// List of commands saved in the replay
    /// </summary>
    private List<TimedBoardCommand> timedCommands;
    private IEnumerator<TimedBoardCommand> replay;
    private TimedBoardCommand timedCommand;

    private bool isPlaying;
    /// <summary>
    /// The time relative to when the replay started
    /// </summary>
    private double time;

    private void Awake()
    {
        timedCommands = new List<TimedBoardCommand>();
    }

    public void Add(TimedBoardCommand command)
    {
        timedCommands.Add(command);
    }

    /// <summary>
    /// Starts the replay
    /// </summary>
    public void BeginReplay()
    {
        if (timedCommands.Count == 0) return;
        time = 0;
        isPlaying = true;
        timedCommands.Sort();
        replay = timedCommands.GetEnumerator();
        replay.MoveNext();
    }

    public void ClearReplay()
    {
        timedCommands.Clear();
    }


    private void Update()
    {
        if (isPlaying)
        {
            timedCommand = replay.Current;
            if (time >= timedCommand.time)
            {
                timedCommand.command.Execute();
                isPlaying = replay.MoveNext();
            }
            time += Time.deltaTime;
        }
    }
}
