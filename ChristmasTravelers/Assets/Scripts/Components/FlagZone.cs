using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagZone : MonoBehaviour
{
    [Tooltip("The index of the player associated with this zone in the game manager player list")]
    [SerializeField] private int playerIndex;
    private Player player;

    private void Start()
    {
        player = GameModeData.selectedMode.players[playerIndex];
        GetComponent<SpriteRenderer>().color = player.color;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
