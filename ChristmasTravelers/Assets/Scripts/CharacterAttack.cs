using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public float atk;
    public float spd;
    public float lifeLength;
    public Projectile projectile;

    public void Shoot(Vector3 direction)
    {
        Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity);
        // TO DO : Système de layers pour chaque joueur histoire d'éviter le tir allié
        proj.transform.position += direction.normalized;
        proj.OnHit += OnHit;
        proj.Shoot(direction, spd, lifeLength);
    }

    public void OnHit(GameObject obj)
    {
        if (obj.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(atk);
        }
    }
}
