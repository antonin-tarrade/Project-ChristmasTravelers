using Items;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player : IPreparable
{

	public Transform spawn;

	public string name; // debug

	public int number;

	public Color color;

	public int score;

	public List<Character> characters { get; private set; }

	public Character charPrefabSystem;

	public void Init()
	{
		characters = new List<Character>();
		score = 0;
		GameManager.instance.Register(this);
	}

	public void Prepare()
	{
		score = 0;
		foreach (Character character in characters)
		{
			character.Prepare();
		}
	}


	public void AddCharacter(Character character)
	{
		characters.Add(character);
		character.GetComponent<SpriteRenderer>().color = this.color;
		character.player = this;
	}

	public Character ChooseCharacter() {
		return charPrefabSystem;
	}



}
