using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Mine : MonoBehaviour, IDamageable, ISpawnable
{
    [SerializeField] private float damage;
    [SerializeField] private float explosionRadius;
    private Character spawner;

    public void Damage(float dmg)
    {
        Explode();
    }

    public void Explode()
    {
        Debug.Log("EXPLOSION");
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
        Debug.Log("???");
        this.spawner = spawner;
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Character>(out Character c) && c.player != spawner.player)
        {
            Explode();
        }
    }
}
