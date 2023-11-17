using Items;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Flag : IMapItem, IGrabbable
{

    [field : SerializeField] public int scorePoints { get; private set; }



    private BoxCollider2D hitbox;

    private bool isCaptured;


    private void Start()
    {
        isCaptured = false;
        mapState = IMapItem.MapItemState.Free;
        hitbox = GetComponent<BoxCollider2D>();
        MapItemInitialData initialData = new MapItemInitialData()
        {
            initialPosition = transform.position,
            initialState = mapState
        };
        ItemManager.instance.Register(this, initialData);
    }

    private void LateUpdate()
    {
        if (mapState == IMapItem.MapItemState.Grabbed)
        {
            transform.position = characterHoldingThis.transform.position + new Vector3(0, 1);
        }
    }

    public void AcceptCollect(Character character)
    {
        if (isCaptured || mapState == IMapItem.MapItemState.Grabbed) return;
        character.GetComponent<Inventory>().Add(this);
        characterHoldingThis = character;
        mapState = IMapItem.MapItemState.Grabbed;
    }


    public override void Initialise()
    {
        isCaptured = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void OnCapture()
    {
        OnCapture(characterHoldingThis.player);
    }

    public void OnCapture(Player player)
    {
        Drop();
        isCaptured = true;
        GetComponent<SpriteRenderer>().color = player.color;
    }
}
