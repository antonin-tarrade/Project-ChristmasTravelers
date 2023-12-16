using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterComponent : MonoBehaviour
{

    public GameObject charPrefab;

    // Start is called before the first frame update

    public CharacterComponent left {get; set;}
    public CharacterComponent right { get; set; }
    public CharacterComponent up { get; set; }
    public CharacterComponent down { get; set; }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
