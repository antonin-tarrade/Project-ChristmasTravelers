using Records;
using UnityEngine;

public class Character : MonoBehaviour {
	public Player player;
	public Replay replay { get; private set; }
	public IRecorder[] recorders { get; private set; }


	[Header("Gameplay parameters")]
    public float grabRadius;

    private void Awake () {
		replay = GetComponent<Replay> ();
		recorders = GetComponents<IRecorder> ();
	}

	public void Prepare(){
		transform.position = player.spawn.transform.position;
	}
}
