using Items;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player {

	public Transform spawn;

	public string name; // debug
	public Color color;

	public int score;

	private List<Character> characters;

	public void Init()
	{
		characters = new List<Character>();
		score = 0;
	}


	public void AddCharacter(Character character)
	{
		characters.Add(character);
		character.GetComponent<SpriteRenderer>().color = color;
	}



}
