using Items;
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
    public Image CharacterImage { get; private set; }
    public Image CurrentObjectImage { get; private set; }
    public GameObject HealthBar { get; private set; }
    public TextMeshProUGUI PlayerName { get; private set; }


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject UI = Instantiate(UIPrefab, transform);
        CharacterImage = UI.transform.Find("CharacterImage").GetComponent<Image>();
        CurrentObjectImage = UI.transform.Find("ObjectImage").GetComponent<Image>();
        PlayerName = UI.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnCharacterChanged(Player player,Character character)
    {
        PlayerName.text = "Player " + player.number.ToString();
        CharacterImage.sprite = character.GetDisplaySprite("big");
    }


    public void OnItemChanged(IItem item)
    {
        CurrentObjectImage.sprite = item.sprite;
    }
}
