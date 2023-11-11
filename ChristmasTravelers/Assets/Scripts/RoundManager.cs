using BoardCommands;
using Records;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundManager : MonoBehaviour {
	[SerializeField] private GameObject playerPrefab;

	// Currently inputed character
	private Character currentCharacter;

	private Player currentPlayer;

	private List<Character> ghosts = new List<Character> ();

	public void StartNextTurn () {
		if (currentPlayer == GameManager.instance.players[0]) {
			StartTurn (GameManager.instance.players[1]);
		} else {
			StartTurn (GameManager.instance.players[0]);
		}
	}

	/// <summary>
	/// Starts a given player turn
	/// </summary>
	public void StartTurn (Player player) {
		currentPlayer = player;

		// Spawns new character under the current player
		currentCharacter = SpawnCharacter (currentPlayer);

		// Starts recordings
		foreach (IRecorder<IBoardCommand> recorder in currentCharacter.recorders) {
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
		if (currentCharacter != null) {
			return;
		}

		// Stops and saves recordings
		foreach (IRecorder<IBoardCommand> recorder in currentCharacter.recorders) {
			recorder.EndRecord ();
			recorder.SaveRecord (currentCharacter.replay);
		}

		// Current character is now a ghost and loses its recorders
		ghosts.Add (currentCharacter);
		foreach (IRecorder<IBoardCommand> recorder in currentCharacter.recorders) {
			// TODO désactiver les composants
		}

		// All ghosts returns to their start positions
		foreach (Character ghost in ghosts) {
			ghost.transform.position = ghost.player.spawn.position;
		}

		// No more current character
		currentCharacter = null;
	}

	/// <summary>
	/// Spawns a character under given player control
	/// </summary>
	private Character SpawnCharacter (Player player) {
		Character spawnCharacter = Instantiate (playerPrefab, player.spawn.position, player.spawn.rotation).GetComponent<Character> ();
		spawnCharacter.player = player;
		spawnCharacter.name = "Character - " + player.name;

		return spawnCharacter;
	}
}