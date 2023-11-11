using BoardCommands;
using Records;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	public Player player;
	public Replay replay { get; private set; }
	public IRecorder<IBoardCommand>[] recorders { get; private set; }

	private void Awake () {
		replay = GetComponent<Replay> ();
		recorders = GetComponents<IRecorder<IBoardCommand>> ();
		Debug.Log (recorders.Length + " recorders found");
		// set recorders
	}
}
