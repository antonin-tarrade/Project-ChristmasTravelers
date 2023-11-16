using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public event Action<GameObject> OnHit;

    private Vector3 direction;
    private float speed;
    private float lifeLength;
    private bool isActive;
    private float time;

    private void Awake()
    {
        isActive = false;
        time = 0;
    }

    public void Shoot(Vector3 d, float s, float ll)
    {
        if (isActive) return;
        isActive = true;
        direction = d;
        speed = s;
        lifeLength = ll;
    }

    private void Update()
    {
        if (isActive)
        {
            transform.position += Time.deltaTime * speed * direction;
            time += Time.deltaTime;
            if (time > lifeLength) OnEnd();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnHit?.Invoke(collision.gameObject);
        OnEnd();
    }

    private void OnEnd()
    {
        Destroy(gameObject);
    }
}