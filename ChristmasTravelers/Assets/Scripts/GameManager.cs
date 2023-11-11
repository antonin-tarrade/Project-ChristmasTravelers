using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	// Playing players
	[field: SerializeField] public List<Player> players { get; private set; }

	private void Awake () {
		instance = this;
	}

    private void Start()
    {
        foreach (Player p in players)
        {
            p.Init();
        }
        RoundManager.instance.OnTurnStart += OnTurnStart;
    }

    private void OnTurnStart()
    {
        foreach (Player p in players)
        {
            p.score = 0;
        }
    }

}
