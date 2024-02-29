using Items;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{

    [Tooltip("The index of the player associated with this zone in the game manager player list")]
    [SerializeField] private int playerIndex;
    [SerializeField, Range(0,1)] private float spriteAlpha;
    private Player player;

    private void Awake()
    {
        player = GameModeData.selectedMode.players[playerIndex];
        player.spawn = transform.position;
        Color c = player.team.teamColor;
        c.a = spriteAlpha;
        GetComponent<SpriteRenderer>().color = c;
    }

    private void Start()
    {
       
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Inventory inv) && inv.GetComponent<Character>().player == player)
        {
            List<IItem> flags = new();
            foreach (IItem item in inv.items.Where(i => i.GetName() == "Flag"))
            {
                flags.Add(item);
            }
            foreach (IItem flag in flags)
            {
                inv.GetComponent<Character>().player.score++;
                flag.Drop();
                flag.container.gameObject.transform.position = transform.position;
                flag.container.gameObject.GetComponent<GrabbableItem>().activated = false;
            }
        }
    }
}

