using Records;
using UnityEngine;

public class Character : MonoBehaviour {
	public Player player;
	public Replay replay { get; private set; }
	public IRecorder[] recorders { get; private set; }

	private void Awake () {
		replay = GetComponent<Replay> ();
		recorders = GetComponents<IRecorder> ();
	}
}
