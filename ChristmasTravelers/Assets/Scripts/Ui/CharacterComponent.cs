using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterComponent : MonoBehaviour
{

    public GameObject charPrefab;

    // Start is called before the first frame update

    public CharacterComponent left;
    public CharacterComponent right;
    public CharacterComponent up;
    public CharacterComponent down;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelect(Player player){
        GetComponent<Image>().color = player.color;
    }

    public void OnDeselect(){
        GetComponent<Image>().color = Color.white;
    }

    
}
