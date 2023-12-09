using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager instance;

    [field : SerializeField] public GameData gameData {  get; private set; }

	// Playing players
	[field: SerializeField] public List<Player> players { get; private set; }


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
    }

    public void Register(IPreparable preparable)
    {
        preparables.Add(preparable);
    }

}
