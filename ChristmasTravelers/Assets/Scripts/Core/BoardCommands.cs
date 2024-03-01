using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace BoardCommands
{

    /// <summary>
    /// Represents a command to execute
    /// </summary>
    public interface IBoardCommand
    {
        void Execute();
    }

    public class MoveBoardCommand : IBoardCommand
    {
        public GameObject obj;
        public Vector3 movement;
        private Character character;

        public MoveBoardCommand(GameObject obj, Vector3 movement)
        {
            this.obj = obj;
            this.movement = movement;
            character = obj.GetComponent<Character>();
        }
        

        public void Execute()
        {
            character.UpdatePosition(movement);
        }
    }

    public class GrabCommand : IBoardCommand
    {
        public Character character;

        public GrabCommand(Character character)
        {
            this.character = character;
        }

        public void Execute()
        {
            if (character.gameObject.layer == LayerMask.NameToLayer("Dead")) return;
            // TO DO : Passer par Character partout
            float grabRadius = character.grabRadius;
            Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(character.transform.position, grabRadius);
            IGrabbable closestItem = null;
            float smallestDistance = float.MaxValue;
            float distance;
            foreach (Collider2D collider in nearbyObjects)
            {
                if (collider.gameObject.TryGetComponent<IGrabbable>(out IGrabbable item))
                {
                    if ((distance = Vector3.Distance(character.gameObject.transform.position, collider.gameObject.transform.position)) < smallestDistance)
                    {
                        smallestDistance = distance;
                        closestItem = item;
                    }
                }
            }
            closestItem?.AcceptCollect(character);
            
        }
    }

    public class ShootCommand : IBoardCommand
    {
        public IAttack attack;

        public ShootCommand(IAttack attack)
        {
            this.attack = attack;
        }

        public void Execute()
        {
            attack.Shoot();
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

        public void Execute()
        {
            attack.shootDirection = direction;
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

        public void Execute()
        {
            item.Use(character.GetComponent<Inventory>(), parameters);
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
