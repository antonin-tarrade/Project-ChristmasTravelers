using Records;
using System;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public class Character : MonoBehaviour {
	public Player player;
	public Replay replay { get; private set; }
	public IRecorder[] recorders { get; private set; }

	[Header("Gameplay parameters")]
    public float grabRadius;
    [field : SerializeField] public int FOV { get; private set; }

    [field: SerializeField] public CharacterAnimator chAnimator{ get; private set; }


    private Rigidbody2D body;

    private void Awake () {
		replay = GetComponent<Replay> ();
		recorders = GetComponents<IRecorder> ();
		body = GetComponent<Rigidbody2D> ();
	}

	public void Prepare(){
		transform.position = player.spawn;
	}


    public void UpdatePosition(Vector2 position)
    {
        chAnimator.NotifyMovement(position - body.position);
        body.position = position;		
    }
}
