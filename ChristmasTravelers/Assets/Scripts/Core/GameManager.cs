using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    // TEMP

    public void temp()
    {
        ReadOnlyArray<InputDevice> devices = InputSystem.devices;
        InputDevice device = devices[0];
        PlayerInput pi;
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    public void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        
    }

    // TEMP END



    //Debug
    public Character[] DEBUG;

    [SerializeField] private CharController charControllerPrefab;

	public static GameManager instance;

    public event Action OnTurnStart;
    public event Action OnTurnEnd;

    public RoundHandler roundHandler;
    public static readonly int defaultFOV = 15;
    
    public CinemachineVirtualCamera virtualCamera;
    [field : SerializeField] public GameData gameData {  get; private set; }
    private GameModeData gameMode;

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
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) Play();
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
        currentPlayer = gameMode.players[i];
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
        
        SwitchTo(currentPlayerIndex);
        Character c = SpawnCharacter(currentPlayer);
        currentPlayer.charController.Set(c.GetComponent<CharacterInput>());
        roundHandler.SwitchTo(c);
        virtualCamera.m_Lens.OrthographicSize = c.FOV;
        OnTurnStart?.Invoke();
        foreach (Player p in gameMode.players)
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
        currentPlayerIndex = (currentPlayerIndex + 1) % gameMode.players.Count;
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
        gameMode = GameModeData.selectedMode;
        foreach (Player p in gameMode.players)
            p.InitBeforeGame();
        SceneManager.LoadScene(gameMode.sceneName);
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameMode != null && scene.name == gameMode.sceneName)
        {
            if (isPlaying) return;
            SpawnCharacterControllers();
            virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
            roundHandler = new RoundHandler(virtualCamera);
            for (int i = 0; i < gameMode.nbOfPlayers; i ++)
            {
                gameMode.players[i].spawn = gameMode.spawns[i];
            }
            StartTurn();
        }
    }

    public void SpawnCharacterControllers()
    {
        Dictionary<Player, PlayerInput> inputs = PlayerInputInfo.CreatePlayerInputs(charControllerPrefab.GetComponent<PlayerInput>(), gameMode.players.ToArray()); 
        foreach (Player player in gameMode.players)
        {
            CharController controller = inputs[player].GetComponent<CharController>();
            player.charController = controller;
        }
    }

    public void OnDeviceLost(InputDevice device)
    {
        Debug.Log(device.name + "lost!");
    }

    public void OnDeviceRegained(InputDevice device)
    {
        Debug.Log(device.name + "regained!");
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
