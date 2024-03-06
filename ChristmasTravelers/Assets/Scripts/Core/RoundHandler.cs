using Cinemachine;
using Records;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoundHandler  {


	private Character activeCharacter;
	public ISet<Character> inactiveCharacters;
	private CinemachineVirtualCamera camera;
	private bool isActive;

    public RoundHandler(CinemachineVirtualCamera camera) {
		activeCharacter = null;
		inactiveCharacters = new HashSet<Character>();
		this.camera = camera;
	}


	public void Add(Character c) {
		inactiveCharacters.Add(c);
	}

	public void SwitchTo(Character c) {
		if (c != null && inactiveCharacters.Contains(c) && c != activeCharacter) {
			inactiveCharacters.Remove(c);
			if (activeCharacter != null) inactiveCharacters.Add(activeCharacter);
			activeCharacter = c;
		}
	}

	/// <summary>
	/// Starts a turn
	/// </summary>
	public void StartTurn () {
		camera.Follow = activeCharacter.transform;
		isActive = true;
        // All ghosts returns to their start positions
        foreach (Character ghost in inactiveCharacters) {
			ghost.Prepare();
		}

		// Current character returns to position
		activeCharacter.Prepare();
        IngameUIManager.instance.Link(activeCharacter);


		// Starts recordings
		foreach (IRecorder recorder in activeCharacter.recorders) {
			recorder.BeginRecord ();
		}

		// Starts replays of ghost characters
		foreach (Character ghost in inactiveCharacters) {
			ghost.replay.BeginReplay ();
		}
	}


	/// <summary>
	/// Ends the current turn
	/// </summary>
	public void EndTurn () {
		
		if (!isActive) return;
        IngameUIManager.instance.Unlink(activeCharacter);
		isActive = false;

		// Stops and saves recordings
		foreach (IRecorder recorder in activeCharacter.recorders) {
			recorder.EndRecord ();
			recorder.SaveRecord (activeCharacter.replay);
		}
	}

}