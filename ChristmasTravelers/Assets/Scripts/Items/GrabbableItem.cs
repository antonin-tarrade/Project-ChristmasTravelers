using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class GrabbableItem : MonoBehaviour, IGrabbable {

    [SerializeField] private ScriptableItemData itemData;
    private IItem item;
    private Vector3 initialPosition;

    private void Start() {
        initialPosition = transform.position;
        item = itemData.GetInstance();
        GameManager.instance.Register(new GrabbableItemInitialData(this, item, initialPosition));
    }


    public void Set(IItem item){
        this.item = item;
    }

    public void AcceptCollect(Character character)
    {
        character.GetComponent<Inventory>().Add(item);
        gameObject.SetActive(false);
    }
        

}
