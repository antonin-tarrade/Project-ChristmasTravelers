using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //Debug
    public Character[] DEBUG;

	public static GameManager instance;

    public event Action OnTurnStart;
    public event Action OnTurnEnd;

    public RoundHandler roundHandler;
    public static readonly int defaultFOV = 15;
    
    public CinemachineVirtualCamera virtualCamera;

    public GameModeData gameMode;
    [field : SerializeField] public GameData gameData {  get; private set; }

	// Playing players
	[field: SerializeField] public List<Player> players { get; private set; }

    private int currentPlayerIndex;

    private Player currentPlayer;

    private List<IPreparable> preparables;

    private bool isPlaying;

	private void Awake () {

        DontDestroyOnLoad(gameObject);
		instance = this;
        currentPlayerIndex = 0;
        preparables = new List<IPreparable>();

        isPlaying = false;
        
	}

    private void Start()
    {
        foreach (Player p in players)
        {
            p.Init();
        }

    }

    private void Update() {
    }

    public void Register(IPreparable p)
    {
        preparables.Add(p);
    }

    public void SwitchTo(int i) {
        currentPlayer = players[i];
    }

    public Character SpawnCharacter(Player p) {
        Character c = Instantiate(p.ChooseCharacter());
        p.AddCharacterInstance(c);
        roundHandler.Add(c);
        return c;
    }

    public void StartTurn()
    {
        if (isPlaying) return;
        isPlaying = true;
        foreach (IPreparable p in preparables)
        {
            p.Prepare();
        }
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
    }

    public void EndTurn() {
        if (!isPlaying) return;
        OnTurnEnd?.Invoke();
        roundHandler.EndTurn();
        isPlaying = false;
    }



    public void Play()
    {
        SceneManager.LoadScene(gameMode.scene.name);
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameMode != null && scene.name == gameMode.scene.name)
        {
            if (isPlaying) return;
            virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
            roundHandler = new RoundHandler(virtualCamera);
            for (int i = 0; i < gameMode.nbOfPlayers; i ++)
            {
                players[i].spawn = gameMode.spawns[i];
            }
            StartTurn();
        }
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
