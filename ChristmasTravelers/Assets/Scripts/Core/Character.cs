using Records;
using UnityEngine;

public class Character : MonoBehaviour {
	public Player player;
	public Replay replay { get; private set; }
	public IRecorder[] recorders { get; private set; }

	[Header("Gameplay parameters")]
    public float grabRadius;
    [field : SerializeField] public int FOV { get; private set; }

    private void Awake () {
		replay = GetComponent<Replay> ();
		recorders = GetComponents<IRecorder> ();
	}

	public void Prepare(){
		Debug.Log(player.spawn);
		Debug.Log(player.name);
		transform.position = player.spawn;
	}
}
