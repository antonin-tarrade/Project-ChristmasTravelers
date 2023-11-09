using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardCommands
{
    /// <summary>
    /// Class that manages any possible command 
    /// </summary>
    public class BoardCommandHandler
    {
        public virtual void Execute(MoveBoardCommand command)
        {
            command.obj.transform.position += command.movement;
        }
    }

    /// <summary>
    /// Represents a command to execute
    /// </summary>
    public interface IBoardCommand
    {
        void ExecuteOn(BoardCommandHandler board);
    }

    public class MoveBoardCommand : IBoardCommand
    {
        public GameObject obj;
        public Vector3 movement;

        public MoveBoardCommand(GameObject obj, Vector3 movement)
        {
            this.obj = obj;
            this.movement = movement;
        }

        public void ExecuteOn(BoardCommandHandler board)
        {
            board.Execute(this);
        }
    }


    public readonly struct TimedBoardCommand : IComparable<TimedBoardCommand>
    {
        public readonly double time;

        public readonly IBoardCommand command;

        public TimedBoardCommand(double time, IBoardCommand command)
        {
            this.time = time;
            this.command = command;
        }

        /// <summary>
        /// Used to sort TimedBoardCommands
        /// </summary>
        /// <param name="timedCommand"></param>
        /// <returns></returns>
        public int CompareTo(TimedBoardCommand timedCommand)
        {
            if (time < timedCommand.time) return -1;
            else if (time > timedCommand.time) return 1;
            else return 0;
        }
    }

}
