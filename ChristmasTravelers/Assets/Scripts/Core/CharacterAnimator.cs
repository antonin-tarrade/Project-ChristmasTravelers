#region Imports
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.U2D.Animation;
#endregion

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteLibrary))]
[RequireComponent(typeof(SpriteResolver))]
public class CharacterAnimator : MonoBehaviour
{
    #region Variables

    private Animator animator;
    private enum Direction {DOWN,UP,LEFT,RIGHT,IDLE};
    private Direction precDirection;
    private SpriteLibrary spriteLibrary;

    #endregion


    #region MonoBehaviour
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        precDirection = Direction.DOWN;
        spriteLibrary = GetComponent<SpriteLibrary>();
    }
    #endregion



    #region Methods

    public void NotifyMovement(Vector2 movement)
    {
        const float movementThreshold = 0.0001f;

        Vector2 adjustedMovement = AdjustMovement(movement);

        Direction direction = GetDirection(adjustedMovement);

        if (direction == precDirection) return;
        precDirection = direction;

        if (adjustedMovement.sqrMagnitude < movementThreshold * movementThreshold)
        {
            animator.SetBool("idle",true);
            
        } else {
            animator.SetBool("idle", false);
            switch (direction)
            {
                case Direction.RIGHT:
                    animator.SetTrigger("right");
                    break;

                case Direction.LEFT:
                    animator.SetTrigger("left");
                    break;

                case Direction.UP:
                    animator.SetTrigger("up");
                    break;

                case Direction.DOWN:
                    animator.SetTrigger("down");
                    break;
            }
        }
       
    }


    private Direction GetDirection(Vector2 movement)
    {

        if (movement.x > 0)
        {
            return Direction.RIGHT;
        } else if (movement.x < 0)
        {
            return Direction.LEFT;
        } else if (movement.y > 0)
        {
            return Direction.UP;
        } else if (movement.y < 0)
        {
            return Direction.DOWN;
        } else 
        {
            return Direction.IDLE; 
        }

    }

    private Vector3 AdjustMovement(Vector2 movement)
    {

        Vector2 adjustedMovement = (Math.Abs(movement.x) > Math.Abs(movement.y) ) ? new Vector2(movement.x,0f) : new Vector2(0f,movement.y);
        return adjustedMovement;

    }

    public void NotifyFlagGrabbed(Character character)
    {
        spriteLibrary.spriteLibraryAsset = character.player.team.chikenSpriteLibrary;
        
    }

    public void NotifyFlagDrop(Character character)
    {
        spriteLibrary.spriteLibraryAsset = character.player.team.spriteLibrary;

    }

    #endregion



}