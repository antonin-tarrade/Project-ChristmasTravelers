using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.TextCore.Text;

public class GrabbableItem : MonoBehaviour, IGrabbable, IItemContainer
{

    [SerializeField] private ScriptableItemData itemData;
    private IItem item;
    private Vector3 initialPosition;
    public bool activated;
    private SpriteRenderer sr;

    private void Start() {
        initialPosition = transform.position;
        Set(itemData.GetInstance());
        GameManager.instance.Register(new GrabbableItemInitialData(this, item, initialPosition));
        sr = GetComponent<SpriteRenderer>();
        activated = true;
    }


    public void Set(IItem item){
        if (this.item != null)
            item.OnDropEvent -= OnItemDropped;
        this.item = item;
        item.OnDropEvent += OnItemDropped;
        item.container = this;
    }

    public void OnItemDropped(IItem item)
    {
        sr.enabled = true;
        activated = true;
        transform.position = item.container.gameObject.transform.position;
        item.container = this;
    }

    public void AcceptCollect(Character character)
    {
        if (!activated) return;
        Inventory inv = character.GetComponent<Inventory>();
        inv.Add(item);
        inv.GetComponent<IDamageable>().OnDeath += () => item?.Drop();
        item.container = inv;
        sr.enabled = false;
        activated = false;
    }

    public bool Contains(IItem item) => this.item == item;

    public void Add(IItem item)
    {
        Set(item);
    }

    public void Remove(IItem item)
    {
        item = null;
    }

    public void SetGameObject(GameObject gameObject)
    { 
    }
}
