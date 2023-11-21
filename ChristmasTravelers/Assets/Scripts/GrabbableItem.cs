using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class GrabbableItem : MonoBehaviour, IGrabbable {

    [SerializeField] private ScriptableItemData itemData;
    private IItem item;
    private Vector3 initialPoisiton;

    private void Start() {
        initialPoisiton = transform.position;
        item = itemData.GetInstance();
        item.RegisterInitialState(ItemManager.instance, this);
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
