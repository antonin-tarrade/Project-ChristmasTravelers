using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Character[] DEBUG;

	public static GameManager instance;

    public event Action OnTurnStart;
    public event Action OnTurnEnd;

    public RoundHandler roundHandler;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public Character prefab;

    [field : SerializeField] public GameData gameData {  get; private set; }

	// Playing players
	[field: SerializeField] public List<Player> players { get; private set; }
    private int currentPlayerIndex;
    private Player currentPlayer;

	private void Awake () {
		instance = this;
        roundHandler = new RoundHandler(virtualCamera);
        currentPlayerIndex = 0;
	}

    private void Start()
    {
        foreach (Player p in players)
        {
            p.Init();
        }

    }

    private void Update() {
        DEBUG = roundHandler.inactiveCharacters.ToArray<Character>();
    }

    public void SwitchTo(int i) {
        currentPlayer = players[i];
    }

    public Character SpawnCharacter(Player p) {
        Character c = Instantiate(p.ChooseCharacter());
        p.AddCharacter(c);
        roundHandler.Add(c);
        return c;
    }

    public void StartTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        SwitchTo(currentPlayerIndex);
        Character c = SpawnCharacter(currentPlayer);
        roundHandler.SwitchTo(c);

        OnTurnStart?.Invoke();
        foreach (Player p in players)
        {
            p.score = 0;
        }
        roundHandler.StartTurn();
    }

    public void EndTurn() {
        OnTurnEnd?.Invoke();
        roundHandler.EndTurn();
    }

}
