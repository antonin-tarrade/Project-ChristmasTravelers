using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, ISpawnable, IDamageable
{
    private int orientation;

    [SerializeField] private float health;
    [SerializeField] private float spawnOffset;

    public void Damage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
            Destroy(gameObject);
    }

    public void Set(Character c, Vector3 position, Vector3 direction)
    {
        c.player.toAvoid.Add(GetComponent<Collider2D>());
        transform.position = position + spawnOffset * direction.normalized;
        SetDirection(direction);
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
