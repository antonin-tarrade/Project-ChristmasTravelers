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
            if (command.character.gameObject.layer == LayerMask.NameToLayer("Dead")) return;
            // TO DO : Passer par Character partout
            float grabRadius = command.character.grabRadius;
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
    
        public virtual void Execute(ShootCommand command)
        {
            command.attack.Shoot();
        }

        public virtual void Execute(UseItemCommand command)
        {
            command.item.Use(command.character.GetComponent<Inventory>(), command.parameters);
        }

        public virtual void Execute(AimCommand command)
        {
            command.attack.shootDirection = command.direction;
        }

        public virtual void Execute(ScheduledCommand command)
        {
            CommandScheduler.instance.Schedule(command);
        }

        public virtual void Execute(MultipleCommand command)
        {
            foreach (IBoardCommand recursiveCommand in command.commands)
            {
                recursiveCommand.ExecuteOn(this);
            }
        }
    }

    /// <summary>
    /// Represents a command to execute
    /// </summary>
    public interface IBoardCommand
    {
        void ExecuteOn(BoardCommandHandler board);
    }

    public class ScheduledCommand : IBoardCommand
    {
        public IBoardCommand command;
        public float time;

        public ScheduledCommand(IBoardCommand command, float time)
        {
            this.command = command;
            this.time = time;
        }

        public void ExecuteOn(BoardCommandHandler board)
        {
            board.Execute(this);
        }
    }

    public class MultipleCommand : IBoardCommand
    {
        public IBoardCommand[] commands;

        public MultipleCommand(IBoardCommand[] commands)
        {
            this.commands = commands;
        }

        public void ExecuteOn(BoardCommandHandler board)
        {
            board.Execute(this);
        }
    }

    public class DelegateCommand : IBoardCommand
    {
        private event Action action;

        public DelegateCommand(Action action)
        {
            this.action = action;
        }

        public void ExecuteOn(BoardCommandHandler board)
        {
            //board.Execute(this);
            // Seule la classe peut utiliser son propre event
            action();
        } 
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

    public class GrabCommand : IBoardCommand
    {
        public Character character;

        public GrabCommand(Character character)
        {
            this.character = character;
        }

        public void ExecuteOn(BoardCommandHandler board)
        {
            board.Execute(this);
        }
    }

    public class ShootCommand : IBoardCommand
    {
        public IAttack attack;

        public ShootCommand(IAttack attack)
        {
            this.attack = attack;
        }

        public void ExecuteOn(BoardCommandHandler board)
        {
            board.Execute(this);
        }
    }

    public class AimCommand : IBoardCommand
    {
        public IAttack attack;
        public Vector3 direction;

        public AimCommand(IAttack attack, Vector3 direction)
        {
            this.attack = attack;
            this.direction = direction;
        }

        public void ExecuteOn(BoardCommandHandler board)
        {
            board.Execute(this);
        }
    }



    public class UseItemCommand : IBoardCommand
    {
        public Character character;
        public IItem item;
        public IItemParameters parameters;

        public UseItemCommand(Character character, IItem item, IItemParameters parameters)
        {
            this.character = character;
            this.item = item;
            this.parameters = parameters;
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