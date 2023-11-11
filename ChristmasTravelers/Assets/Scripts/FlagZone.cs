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
        player = GameManager.instance.players[playerIndex];
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TRIGGER");
        if (collision.gameObject.TryGetComponent<Flag>(out Flag flag))
        {
            Debug.Log("FLAG");
            if (flag.characterHoldingThis.player == player)
            {
                Debug.Log("PLAYER");
                player.score += flag.scorePoints;
                flag.Drop();
            }
        }
    }
}
