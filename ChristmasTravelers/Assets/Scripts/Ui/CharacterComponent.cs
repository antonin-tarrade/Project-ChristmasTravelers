using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterComponent : MonoBehaviour
{

    public GameObject charPrefab{get; set;}
    public Tuple<int,int> position{get; set;}

    public CharacterComponent right;
    public CharacterComponent left;
    public CharacterComponent up;
    public CharacterComponent down;

    // Start is called before the first frame update
    void Start()
    {
        right = null;
        left = null;
        up = null;
        down = null;
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
