using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHealth : MonoBehaviour, IDamageable
{

    [field : SerializeField] public float baseHealth { get; private set; }
    public float health { get; private set; }

    private HealthBar healthBar;
    public Action OnDeath { get; set; }

    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private void Start()
    {
        Init();
        GameManager.instance.OnTurnStart += Init;
    }

    public void Damage(float dmg)
    {
        health -= dmg;
        GetComponent<Character>().healthBar.Change(-dmg);
        if (health <= 0) Death();
        else StartCoroutine(DamageFeedBack());
    }

    private void Init()
    {
        
        gameObject.layer = LayerMask.NameToLayer("Alive");
        GetComponent<SpriteRenderer>().material = GameManager.instance.gameData.LivingMaterial;
        health = baseHealth;
        healthBar.InitBar(baseHealth);
    }

    private void Death()
    {
        gameObject.layer = LayerMask.NameToLayer("Dead");
        GetComponent<SpriteRenderer>().material = GameManager.instance.gameData.DeadMaterial;
        OnDeath?.Invoke();
    }


    [Header("Damage feedback")]
    [SerializeField] private Color color;
    [SerializeField] private float time;
    private IEnumerator DamageFeedBack()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = color;
        yield return new WaitForSeconds(time);
        sr.color = Color.white;
        
    }

    private void OnDestroy()
    {
        GameManager.instance.OnTurnStart -= Init;
    }
}
