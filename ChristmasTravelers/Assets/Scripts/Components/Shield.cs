using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour, ISpawnable, IDamageable
{
    private int orientation;

    [SerializeField] private float health;
    [SerializeField] private float spawnOffset;
    private Character character;

    public event Action OnDeath;
    public event Action OnDamage;

    public void Damage(float dmg)
    {
        OnDamage?.Invoke();
        health -= dmg;
        if (health <= 0)
        {
            Destroy();
        }
    }

    public void Set(Character character, Vector3 position, Vector3 direction)
    {
        this.character = character;
        Collider2D collider = GetComponent<Collider2D>();
        character.player.toAvoid.Add(collider);
        foreach (Collider2D c in GameModeDataUtility.AllCharactersColliders())
                Physics2D.IgnoreCollision(collider, c);
        transform.position = position + spawnOffset * direction.normalized;
        SetDirection(direction);
    }

    public void Destroy()
    {
        OnDeath?.Invoke();
        character.player.toAvoid.Remove(GetComponent<Collider2D>());
        Destroy(gameObject);
    }

    private void SetDirection(Vector3 direction)
    {
        //temp
        transform.up = direction;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
