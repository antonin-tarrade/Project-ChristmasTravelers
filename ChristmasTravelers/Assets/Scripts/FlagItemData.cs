using Items;
using UnityEngine;


public class FlagItem : Item
{
    protected new FlagItemData data;
    public override string GetName()
    {
        return "Flag";
    }
    public override void OnUse(Inventory inventory)
    {
        Debug.Log("USE FLAG");
    }

    public FlagItem(FlagItemData data) : base(data)
    {
        this.data = data;
    }
}

[CreateAssetMenu(fileName = "FlagItem", menuName = "Scriptables/Items/Flag")]
public class FlagItemData : ScriptableItemData
{
    public override IItem GetInstance()
    {
        return new FlagItem(this);
    }
}