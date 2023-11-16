using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHealth : MonoBehaviour, IDamageable
{

    [field : SerializeField] public float baseHealth { get; private set; }
    public float health { get; private set; }

    private void Awake()
    {

    }

    private void Start()
    {
        Init();
        RoundManager.instance.OnTurnStart += Init;
    }

    public void Damage(float dmg)
    {
        health -= dmg;
        if (health <= 0) Death();
    }

    private void Init()
    {
        gameObject.layer = LayerMask.NameToLayer("Alive");
        GetComponent<SpriteRenderer>().material = GameManager.instance.gameData.LivingMaterial;
        health = baseHealth;
    }

    private void Death()
    {
        gameObject.layer = LayerMask.NameToLayer("Dead");
        GetComponent<SpriteRenderer>().material = GameManager.instance.gameData.DeadMaterial;
    }
}
