using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Mine : MonoBehaviour, IDamageable, ISpawnable
{
    [SerializeField] private float damage;
    [SerializeField] private float explosionRadius;
    private Character spawner;

    public event Action OnDeath;
    public event Action OnDamage;

    public void Damage(float dmg)
    {
        OnDamage?.Invoke();
        Explode();
    }

    

    public void Explode()
    {
        OnDeath?.Invoke();
        Collider2D[] casualties = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D c in casualties)
        {
            if (c.gameObject != gameObject && c.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.Damage(damage);
            }
        }
        Destroy(gameObject);
    }

    public void Set(Character spawner, Vector3 position, Vector3 direction)
    {
        this.spawner = spawner;
        Collider2D collider = GetComponents<Collider2D>().Where(c => !c.isTrigger).ToArray()[0];
        foreach (Collider2D c in GameModeDataUtility.AllCharactersColliders())
        {
            Physics2D.IgnoreCollision(collider, c);
            Debug.Log(c.gameObject.name);
        }
            
        transform.position = position;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Character>(out Character c) && c.player != spawner.player && c.gameObject.layer != LayerMask.NameToLayer("Dead"))
        {
            Explode();
        }
    }
}
