using Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class Player : IPreparable
{

	public Vector3 spawn;

	public string name;

	public int number;

	public Color color;

	public GameModeData.Teams team;

	public int index => GameModeData.selectedMode.players.IndexOf(this);

	 public int score;

	[field : SerializeField] public List<Character> characterPrefabs { get; private set; }
	public List<Character> characterInstances { get; private set; }
	private int currentChar;

	[HideInInspector] public List<Collider2D> toAvoid;

	[HideInInspector] public PlayerController controller;
	[HideInInspector] public CharController charController;
	public PlayerInputInfo inputInfo;

	public void InitBeforeSelection()
	{
		toAvoid = new List<Collider2D>();
		characterPrefabs = new List<Character>();
		characterInstances = new List<Character>();
	}

	public void InitBeforeGame()
	{
        score = 0;
        currentChar = 0;
		characterInstances = new();
		Debug.Log(GameManager.instance);
		Debug.Log(this);
        GameManager.instance.Register(this);
    }

	public void Prepare()
	{
		score = 0;
		toAvoid.Clear();
	}


	public void AddCharacterInstance(Character character)
	{
		characterInstances.Add(character);
		// character.GetComponent<SpriteRenderer>().color = this.color;
		character.player = this;
	}

	public void AddCharacterPrefab(Character character)
	{
		characterPrefabs.Add(character);
	}

	public Character ChooseCharacter() {	
		Character character = characterPrefabs[currentChar];
		currentChar++;
		return character;
	}

}
[Serializable]

public class PlayerInputInfo
{
	// Static methods to generate infos from scratch
	private static int currentIndex;
	private static string defaultScheme => "Controller";
	private static void Initialize()
	{
		currentIndex = 0;
	}

	private static PlayerInputInfo GetNewInfo()
	{
		return new PlayerInputInfo(InputSystem.devices.Where(d => GameModeData.selectedMode.allowedDevices.Contains(d.description.deviceClass)).ToArray()[currentIndex], defaultScheme, currentIndex, -1);
	}

	private static PlayerInput CreatePlayerInput(PlayerInput input, PlayerInputInfo info)
	{
        PlayerInput controller =
            PlayerInput.Instantiate(input.gameObject, info.index, info.scheme, info.splitScreenIndex, info.device)
            .GetComponent<PlayerInput>();
		return controller;
    }

	public static Dictionary<Player,PlayerInput> CreatePlayerInputs(PlayerInput input, Player[] players)
	{
		Dictionary<Player, PlayerInput> map = new();
		if (!GameModeData.selectedMode.overrideControllers)
		{
			foreach (Player p in players)
				map.Add(p, CreatePlayerInput(input, p.inputInfo));
		}
		else
		{
			Initialize();
			foreach (Player p in players)
			{
				PlayerInputInfo info = GetNewInfo();
                currentIndex++;
                p.inputInfo = info;
				map.Add(p, CreatePlayerInput(input, info));
			}
		}
		return map;
	}

	public InputDevice device;
	public string scheme;
	public int index;
	public int splitScreenIndex;
	public string deviceClass;

	public PlayerInputInfo(InputDevice device, string scheme, int index, int splitScreenIndex)
	{
		this.device = device;
		this.scheme = scheme;
		this.index = index;
		this.splitScreenIndex = splitScreenIndex;
		deviceClass = device.description.deviceClass;
	}
}
