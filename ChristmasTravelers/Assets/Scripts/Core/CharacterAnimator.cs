#region Imports
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

    #endregion


    #region MonoBehaviour
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        precDirection = Direction.DOWN;
    }
    #endregion



    #region Methods

    public void NotifyMovement(Vector3 movement)
    {
        const float movementThreshold = 0.01f;

        Direction direction = GetDirection(movement);

        if (direction == precDirection) return;
        precDirection = direction;

        if (movement.sqrMagnitude < movementThreshold * movementThreshold)
        {
            animator.SetBool("idle",true);
            

        } else {
            animator.SetBool("idle", false);
            if (movement.x > 0)
            {
                animator.SetTrigger("right");

            } else if (movement.x < 0) 
            {
                animator.SetTrigger("left");
            }
            else if (movement.y > 0)
            {
                animator.SetTrigger("up");
            } else
            {
                animator.SetTrigger("down");
            }

        }
       
    }


    private Direction GetDirection(Vector3 movement)
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



    #endregion



}