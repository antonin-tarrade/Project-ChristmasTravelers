using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    public static MinimapManager instance { get; private set; }

    private Dictionary<Character, SpriteDuplicator> duplicates;

    private GameManager gameManager;

    private void Awake()
    {
        instance = this;
        duplicates = new();
        gameManager = GameManager.instance;
        gameManager.OnCharacterControlled += OnCharacterControlled;
        gameManager.OnCharacterSpawned += OnCharacterSpawned;
    }

    private void Start()
    {
        
    }

    private void OnCharacterSpawned(Character c)
    {
        duplicates.Add(c, SpriteDuplicator.Duplicate(c.GetComponent<SpriteRenderer>()));
    }

    private void OnCharacterControlled(Character c)
    {
        foreach (KeyValuePair<Character, SpriteDuplicator> kvp in duplicates)
        {
            if (kvp.Key.player == c.player)
                kvp.Value.gameObject.SetActive(true);
            else
                kvp.Value.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        
    }

    private void OnDestroy()
    {
        //gameManager.OnCharacterControlled -= OnCharacterControlled;
        //gameManager.OnCharacterSpawned -= OnCharacterSpawned;
    }


}
