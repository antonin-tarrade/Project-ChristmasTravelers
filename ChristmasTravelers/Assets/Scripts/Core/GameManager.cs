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
    public static readonly int defaultFOV = 15;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;


    [field : SerializeField] public GameData gameData {  get; private set; }

	// Playing players
	[field: SerializeField] public List<Player> players { get; private set; }
    private int currentPlayerIndex;
    private Player currentPlayer;


    private List<IPreparable> preparables;
    private List<GameObject> spawnables;

	private void Awake () {
		instance = this;
        roundHandler = new RoundHandler(virtualCamera);
        currentPlayerIndex = 0;
        preparables = new List<IPreparable>();
        spawnables = new List<GameObject>();
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

    public void Register(IPreparable p)
    {
        preparables.Add(p);
    }

    public void ScheduleDestroy(GameObject s)
    {
        spawnables.Add(s);
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
        foreach (IPreparable p in preparables)
        {
            p.Prepare();
        }
        foreach (GameObject s in spawnables)
        {
            if (s != null) Destroy(s);
        }
        spawnables.Clear();
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        SwitchTo(currentPlayerIndex);
        Character c = SpawnCharacter(currentPlayer);
        roundHandler.SwitchTo(c);
        virtualCamera.m_Lens.OrthographicSize = c.FOV;
        OnTurnStart?.Invoke();
        foreach (Player p in players)
        {
            p.score = 0;
        }
        roundHandler.StartTurn();

        timerEnd = null;
        timerEnd += EndTurn;
        StartCoroutine(Timer(10));
        Debug.Log("START");
    }

    public void EndTurn() {
        OnTurnEnd?.Invoke();
        roundHandler.EndTurn();

        timerEnd = null;
        timerEnd += StartTurn;
        StartCoroutine(Timer(3));
        Debug.Log("END");
    }



    private Action timerEnd;

    private IEnumerator Timer(float time)
    {
        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime;
            yield return null;
        }
        timerEnd?.Invoke();
    }

}
