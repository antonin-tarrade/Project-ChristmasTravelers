using Records;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour {
	
	[SerializeField] private GameObject characterPrefab;

	private IEnumerator<Player> players;						// Enumerator of playing players
	private Character currentCharacter;							// Currently inputed character
	private List<Character> ghosts = new List<Character> ();	// All previous characters

	private void Start () {
		players = GameManager.instance.players.GetEnumerator ();
	}

	/// <summary>
	/// Starts next player turn
	/// </summary>
	public void StartNextTurn () {
		if (currentCharacter != null) {
			Debug.LogWarning ("Previous turn was not ended");
			return;
		}

		if (!players.MoveNext ()) {
			players.Reset ();
			players.MoveNext ();
		}
		StartTurn (players.Current);
	}

	/// <summary>
	/// Starts a given player turn
	/// </summary>
	private void StartTurn (Player player) {
		// All ghosts returns to their start positions
		foreach (Character ghost in ghosts) {
			ghost.transform.position = ghost.player.spawn.position;
		}

		// Spawns new character under the current player
		currentCharacter = SpawnCharacter (player);

		// Starts recordings
		foreach (IRecorder recorder in currentCharacter.recorders) {
			recorder.BeginRecord ();
		}

		// Starts replays of ghost characters
		foreach (Character ghost in ghosts) {
			ghost.replay.BeginReplay ();
		}
	}

	/// <summary>
	/// Ends the current turn
	/// </summary>
	public void EndTurn () {
		if (currentCharacter == null) {
			Debug.LogWarning ("No turn to be ended");
			return;
		}

		// Stops and saves recordings
		foreach (IRecorder recorder in currentCharacter.recorders) {
			recorder.EndRecord ();
			recorder.SaveRecord (currentCharacter.replay);
		}

		// Current character is now a ghost
		ghosts.Add (currentCharacter);
		foreach (IRecorder recorder in currentCharacter.recorders) {
			((MonoBehaviour)recorder).enabled = false;
		}
		currentCharacter = null;
	}

	/// <summary>
	/// Spawns a character under given player control
	/// </summary>
	private Character SpawnCharacter (Player player) {
		Character spawnCharacter = Instantiate (characterPrefab, player.spawn.position, player.spawn.rotation).GetComponent<Character> ();
		spawnCharacter.player = player;
		spawnCharacter.name = "Character - " + player.name;

		return spawnCharacter;
	}
}