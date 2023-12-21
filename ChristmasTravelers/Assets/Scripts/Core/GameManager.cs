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
    private List<GameObject> spawnables;

    private bool isPlaying;

	private void Awake () {

        DontDestroyOnLoad(gameObject);
		instance = this;
        currentPlayerIndex = 0;
        preparables = new List<IPreparable>();
        spawnables = new List<GameObject>();

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

    public void ScheduleDestroy(GameObject s)
    {
        spawnables.Add(s);
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
        StartCoroutine(Timer(gameMode.roundDuration));
        Debug.Log("START");
    }

    public void EndTurn() {
        if (!isPlaying) return;
        OnTurnEnd?.Invoke();
        roundHandler.EndTurn();

        timerEnd = null;
        timerEnd += StartTurn;
        StartCoroutine(Timer(1));
        Debug.Log("END");
        isPlaying = false;
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
    public void Play()
    {
        SceneManager.LoadScene(gameMode.sceneName);
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameMode != null && scene.name == gameMode.sceneName)
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
