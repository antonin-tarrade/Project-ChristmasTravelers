using Items;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class IngameUIManager : MonoBehaviour
{

    // Singleton
    public static IngameUIManager instance;
    [field: SerializeField] public GameObject UIPrefab { get; private set; }
    [field: SerializeField] public Sprite NoObject { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Timer { get; private set; }


    public Image CharacterImage { get; private set; }
    public Image CurrentObjectImage { get; private set; }
    public HealthBar HealthBar { get; private set; }
    public TextMeshProUGUI PlayerName { get; private set; }


    private void Awake()
    {
        instance = this;
        GameObject UI = Instantiate(UIPrefab, transform);
        CharacterImage = UI.transform.GetChild(1).Find("CharacterImage").GetComponent<Image>();
        CurrentObjectImage = UI.transform.GetChild(2).Find("ObjectImage").GetComponent<Image>();
        PlayerName = UI.GetComponentInChildren<TextMeshProUGUI>();
        HealthBar = UI.GetComponentInChildren<HealthBar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Link(Character character){
        character.isLinked = true;
        Link(character.GetComponent<Inventory>());
    }

    public void Unlink(Character character){
        character.isLinked = false;
        UnLink(character.GetComponent<Inventory>());

    }

    public void Link(Inventory inventory){
        inventory.isLinked = true;
    }

    public void UnLink(Inventory inventory){
        inventory.isLinked = false;
    }


    public void NotifyDamage(float dmg){
        HealthBar.Change(-dmg);
    }

    public void InitHealthBar(float baseHealth){
        HealthBar.InitBar(baseHealth);
    }

    public void OnCharacterChanged(Player player,Character character)
    {

        PlayerName.text = "Player " + player.number.ToString();
        CharacterImage.sprite = character.GetDisplaySprite("big");
    }


    public void OnItemChanged(IItem item)
    {
        CurrentObjectImage.sprite = (item == null)? NoObject : ((Item) item).data.sprite;
    }
}
