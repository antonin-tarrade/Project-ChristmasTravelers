using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	[field: SerializeField] public List<Player> players { get; private set; }

	private void Awake () {
		instance = this;
	}
}
