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

    public static GameManager instance;

    // Events
    public event Action OnTurnStart;
    public event Action OnTurnEnd;
    public event Action<Character> OnCharacterSpawned;
    public event Action<Character> OnCharacterControlled;


    // Fields
    [Header("Game global parameters")]
    [SerializeField] private CharController charControllerPrefab;
    [field: SerializeField] public GameData gameData { get; private set; }
    private RoundHandler roundHandler;
    [HideInInspector] public CinemachineVirtualCamera virtualCamera;
    private GameModeData gameMode;


    // State fields
    private Player currentPlayer;
    private List<IPreparable> preparables;
    private List<ISpawnable> spawnables;


    // State parameters
    private bool isPlaying;
    private int nbRounds;
    private int currentPlayerIndex;



    // Public methods

    /// <summary>
    /// Launches the game based on the selected game mode data
    /// </summary>
    public void Play()
    {
        preparables = new();
        spawnables = new();
        gameMode = GameModeData.selectedMode;
        foreach (Player p in gameMode.players)
            p.InitBeforeGame();
        currentPlayerIndex = 0;
        nbRounds = 0;
        OnTurnStart = null;
        OnTurnEnd = null;
        OnCharacterControlled = null;
        OnCharacterSpawned = null;
        SceneManager.LoadScene(gameMode.sceneName);
    }
    /// <summary>
    /// Register an object to be called every round
    /// </summary>
    /// <param name="p">The object to be prepared every game</param>
    public void Register(IPreparable p)
    {
        preparables.Add(p);
    }
    /// <summary>
    /// Schedule the destruction of a gameobject upon round end
    /// </summary>
    /// <param name="s">The object to be destroyed</param>
    public void ScheduleDestroy(ISpawnable s)
    {
        spawnables.Add(s);
    }













    // Private methods



    // MONOBEHAVIOUR
    private void Awake()
    {

        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            isPlaying = false;
        }
    }

    private void Update()
    {
        // Debug
        if (Input.GetKeyDown(KeyCode.P)) Play();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }








    // GAME LOGIC
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameMode != null && scene.name == gameMode.sceneName)
        {
            if (isPlaying) return;
            SpawnCharacterControllers();
            virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
            roundHandler = new RoundHandler(virtualCamera);
            StartTurn();
        }
    }

    private void StartTurn()
    {
        if (isPlaying) return;
        nbRounds++;
        isPlaying = true;
        
        SwitchTo(currentPlayerIndex);
        Character c = SpawnCharacter(currentPlayer);
        ControlCharacter(c);
        OnTurnStart?.Invoke();


        foreach (IPreparable p in preparables)
        {
            p.Prepare();
        }
        foreach (ISpawnable s in spawnables)
        {
            s.Destroy();
        }
        spawnables.Clear();
        roundHandler.StartTurn();

        timerEnd = null;
        timerEnd += EndTurn;
        StartCoroutine(Timer(gameMode.roundDuration));
    }

    private void EndTurn() {
        if (!isPlaying) return;
        OnTurnEnd?.Invoke();
        roundHandler.EndTurn();
        timerEnd = null;
        isPlaying = false;

        if (nbRounds >= gameMode.roundsNumber * gameMode.nbOfPlayers)
        {
            EndGame();
            return;
        }
        timerEnd += StartTurn;
        StartCoroutine(Timer(1));
        currentPlayerIndex = (currentPlayerIndex + 1) % gameMode.players.Count;
        
        

    }

    private void EndGame()
    {
        SceneManager.LoadScene("ChooseGameMode");
    }







    // STATE MANAGEMENT

    private void SwitchTo(int i)
    {
        currentPlayer = gameMode.players[i];
    }

    private void ControlCharacter(Character c)
    {
        currentPlayer.charController.Set(c.GetComponent<CharacterInput>());
        roundHandler.SwitchTo(c);
        virtualCamera.m_Lens.OrthographicSize = c.FOV;
        OnCharacterControlled?.Invoke(c);
    }

    private Character SpawnCharacter(Player p)
    {
        Character c = Instantiate(p.ChooseCharacter());
        p.AddCharacterInstance(c);
        roundHandler.Add(c);
        OnCharacterSpawned?.Invoke(c);
        return c;
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


    // Utility
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
