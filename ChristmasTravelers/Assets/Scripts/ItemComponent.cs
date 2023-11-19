using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ItemComponent : ScriptableObject
{

    private IItem item;
    public IItem Item { get => CreateItem(); set => item = value;} // A changer car cree un item a chaque fois

    public virtual IItem CreateItem() {
        return item;
    }

}
