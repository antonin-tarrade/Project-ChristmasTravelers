using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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

        public virtual void Execute(GrabCommand command)
        {
            // TO DO : Passer par Character partout
            float grabRadius = command.character.GetComponent<InventoryInput>().grabRadius;
            Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(command.character.transform.position, grabRadius);
            IGrabbable closestItem = null;
            float smallestDistance = float.MaxValue;
            float distance;
            foreach (Collider2D collider in nearbyObjects)
            {
                if (collider.gameObject.TryGetComponent<IGrabbable>(out IGrabbable item))
                {
                    if ((distance = Vector3.Distance(command.character.gameObject.transform.position, collider.gameObject.transform.position)) < smallestDistance)
                    {
                        smallestDistance = distance;
                        closestItem = item;
                    }
                }
            }
            closestItem?.AcceptCollect(command.character);
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


    // TO DO : Remplacer le gameobject par le character
    public class GrabCommand : IBoardCommand
    {
        public GameObject character;

        public GrabCommand(GameObject character)
        {
            this.character = character;
        }

        public void ExecuteOn(BoardCommandHandler board)
        {
            board.Execute(this);
        }
    }

    /// <summary>
    /// Represents a command that was executed at a specific time
    /// </summary>
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
