using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.TextCore.Text;
using System;

public class GrabbableItem : MonoBehaviour, IGrabbable, IItemContainer
{
    public static event Action<GrabbableItem> OnItemGrabbed;

    [SerializeField] private ScriptableItemData itemData;
    private IItem item;
    private Vector3 initialPosition;
    public bool activated;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
        activated = true;
    }

    private void Start() {
        Set(itemData.GetInstance());
        GameManager.instance.Register(new GrabbableItemInitialData(this, item, initialPosition));   
    }


    public void Set(IItem item){
        if (this.item != null)
            this.item.OnDropEvent -= OnItemDropped;
        this.item = item;
        item.OnDropEvent += OnItemDropped;
        item.container = this;
        sr.enabled = true;
        activated = true;
    }

    public void OnItemDropped(IItem item)
    {
        sr.enabled = true;
        activated = true;
        transform.position = item.container.gameObject.transform.position;

         /*if (item is FlagItem){
            item.container.gameObject.GetComponent<Character>().OnFlagDropped();
         }*/
        item.container = this;

        OnDrop.Invoke();
        transform.SetParent(null);

       
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
        transform.SetParent(character.transform);
        OnItemGrabbed?.Invoke(this);
    }

    public bool Contains(IItem item) => this.item == item;

    public void Add(IItem item)
    {
        Set(item);
    }

    public void Remove(IItem item)
    {
        if (this.item != null && this.item == item)
        {
            this.item.OnDropEvent -= OnItemDropped;
            this.item = null;
        }
    }


}

public struct GrabbableItemInitialData : IPreparable
{
    public GrabbableItem grabbable;
    public IItem item;
    public Vector3 initialPosition;

    public GrabbableItemInitialData(GrabbableItem grabbable, IItem item, Vector3 initialPosition)
    {
        this.grabbable = grabbable;
        this.item = item;
        this.initialPosition = initialPosition;
    }

    public void Prepare()
    {
        grabbable.transform.position = initialPosition;
        grabbable.activated = true;
        grabbable.transform.SetParent(null);
        grabbable.Set(item);
    }
}
