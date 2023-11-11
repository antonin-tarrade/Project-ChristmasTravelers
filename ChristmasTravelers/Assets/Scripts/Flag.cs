using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour, IGrabbable, IItem
{
    public enum State { Free, Grabbed }
    public State state { get; private set; }

    // GameObject -> character
    private GameObject characterHoldingThis;

    private BoxCollider2D hitbox;

    private List<GameObject> charactersInRange;



    private void Start()
    {
        state = State.Free;
        hitbox = GetComponent<BoxCollider2D>();
    }

    public void AcceptCollect(GameObject character)
    {
        if (state == State.Grabbed) return;
        character.GetComponent<Inventory>().Add(this);
        characterHoldingThis = character;
        state = State.Grabbed;
        hitbox.enabled = false;
    }

    public void Use(Inventory inventory)
    {
        
    }

    public void Drop()
    {
        transform.position = characterHoldingThis.transform.position;  
        characterHoldingThis = null;
        characterHoldingThis.GetComponent<Inventory>().Remove(this);
        state = State.Free;
        hitbox.enabled = true;
    }


    private void LateUpdate()
    {
        if (state == State.Grabbed)
        {
            transform.position = characterHoldingThis.transform.position + new Vector3(0, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Inventory>(out Inventory inventory))
        {
            charactersInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Inventory>(out Inventory inventory))
        {
            charactersInRange.Remove(collision.gameObject);
        }
    }
}
