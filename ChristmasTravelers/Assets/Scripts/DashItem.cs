using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

[CreateAssetMenu(fileName = "DashItem", menuName = "Scriptables/Items/Dash")]
public class DashItem : ConsumableItem
{
    [Header("Dash Settings")]
    
    [SerializeField] float dashSpeed;
    [SerializeField]float dashTime;
    private ItemData data;

    public DashItem(float dashSpeed, float dashTime){
        this.dashSpeed = dashSpeed;
        this.dashTime = dashTime;
    }

    override protected void OnUse(Inventory inventory){
        Character character = inventory.GetComponent<Character>();
        character.GetComponent<DashComponent>().Dash(dashSpeed, dashTime);
    }

}
