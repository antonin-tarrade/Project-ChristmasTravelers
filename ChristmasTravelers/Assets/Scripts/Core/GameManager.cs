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


    private List<IPreparable> preparables;


    private List<IPreparable> preparables;

	private void Awake () {
		instance = this;
        preparables = new();
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
        foreach (IPreparable p in preparables)
        {
            p.Prepare();
        }
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

    public void Register(IPreparable preparable)
    {
        preparables.Add(preparable);
    }

    public void Register(IPreparable preparable)
    {
        preparables.Add(preparable);
    }

}
