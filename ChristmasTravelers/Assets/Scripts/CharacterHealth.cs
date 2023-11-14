using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHealth : MonoBehaviour, IDamageable
{
    public event Action OnDeath;

    [field : SerializeField] public float baseHealth { get; private set; }
    public float health { get; private set; }

    private void Awake()
    {
        health = baseHealth;
    }

    public void Damage(float dmg)
    {
        health -= dmg;
        if (health <= 0) Death();
    }

    private void Death()
    {
        OnDeath?.Invoke();



        // Attention, tuer ne doit pas détruire mais juste "retirer" le personnage du jeu
        Destroy(gameObject);
        Debug.LogWarning("Careful, you should not destroy objects");
    }
}
