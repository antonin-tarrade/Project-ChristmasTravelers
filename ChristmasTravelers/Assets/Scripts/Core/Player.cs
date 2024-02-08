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

	[HideInInspector] public int score;

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
		characterInstances.Clear();
        GameManager.instance.Register(this);
    }

	public void Prepare()
	{
		score = 0;
		foreach (Character character in characterInstances)
		{
			//character.Prepare();
		}
		toAvoid.Clear();
	}


	public void AddCharacterInstance(Character character)
	{
		characterInstances.Add(character);
		character.GetComponent<SpriteRenderer>().color = this.color;
		character.player = this;
	}

		public void AddCharacterPrefab(Character character)
	{
		characterPrefabs.Add(character);
		character.player = this;
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
	private static String defaultScheme => "Controller";
	private static String defaultClass => "Controller";
	private static void Initialize()
	{
		currentIndex = 0;
	}

	private static PlayerInputInfo GetNewInfo()
	{
		Debug.Log("All devices size : " + InputSystem.devices.Count);
		Debug.Log("All gamepads size : " + InputSystem.devices.Where(d => d.description.deviceClass == defaultClass).ToArray().Count());
		return new PlayerInputInfo(InputSystem.devices.Where(d => d.description.deviceClass == defaultClass).ToArray()[currentIndex], defaultScheme, currentIndex, -1);
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
		if (players.Where(p => p.inputInfo != null).Count() == players.Count())
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
				p.inputInfo = info;
				map.Add(p, CreatePlayerInput(input, info));
			}
		}
		return map;
	}

	public InputDevice device;
	public String scheme;
	public int index;
	public int splitScreenIndex;

	public PlayerInputInfo(InputDevice device, String scheme, int index, int splitScreenIndex)
	{
		this.device = device;
		this.scheme = scheme;
		this.index = index;
		this.splitScreenIndex = splitScreenIndex;
	}
}
