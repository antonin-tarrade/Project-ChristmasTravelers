using Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class Player : IPreparable
{

	public Vector3 spawn;

	public string name;

	public int number;

	public Color color;

	public int score;

	public List<Character> characterPrefabs { get; private set; }
	public List<Character> characterInstances { get; private set; }
	private int currentChar;

	public List<Collider2D> toAvoid;

	public PlayerController controller;

	public void Init()
	{
		toAvoid = new List<Collider2D>();
		characterPrefabs = new List<Character>();
		characterInstances = new List<Character>();
		score = 0;
		currentChar = 0;
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
