using BoardCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandScheduler : MonoBehaviour
{

    public static CommandScheduler instance;
    
    private List<ScheduledCommand> schedule;
    private List<ScheduledCommand> toRemove;

    private void Awake()
    {
        instance = this;
        schedule = new List<ScheduledCommand>();
        toRemove = new List<ScheduledCommand>();
    }

    public void Schedule(ScheduledCommand command)
    {
        schedule.Add(command);
    }


    private void Update()
    {
        toRemove.Clear();
        foreach (ScheduledCommand command in schedule)
        {
            command.time -= Time.deltaTime;
            if (command.time <= 0)
            {
                BoardManager.instance.Execute(command.command);
                toRemove.Add(command);
            }
        }
        foreach (ScheduledCommand command in toRemove)
        {
            schedule.Remove(command);
        }
    }
}
