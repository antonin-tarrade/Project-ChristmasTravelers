using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public event Action<GameObject> OnHit;
    public event Action<Projectile> OnEnd;

    protected Vector3 direction;
    private float speed;
    private float lifeLength;
    private bool isActive;
    private float time;
    protected Character character;

    private void Awake()
    {
        isActive = false;
        time = 0;
    }

    public void SetCharacter(Character character)
    {
        this.character = character;
    }

    public void Shoot(Vector3 d, float s, float ll)
    {
        if (isActive) return;
        isActive = true;
        transform.up = d;
        direction = d;
        speed = s;
        lifeLength = ll;
    }

    private void Update()
    {
        Move();
    }

    protected void Move()
    {
        if (isActive)
        {
            transform.up = direction;
            transform.position += Time.deltaTime * speed * direction.normalized;
            time += Time.deltaTime;
            if (time > lifeLength) End();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ProjectileNonCollidable"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.collider);
            return;
        }
        OnHit?.Invoke(collision.gameObject);
        End();
    }

    private void End()
    {
        OnEnd?.Invoke(this);
        Destroy(gameObject);
    }
}
