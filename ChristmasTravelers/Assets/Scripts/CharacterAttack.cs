using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour, IAttack
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
        proj.gameObject.layer = gameObject.layer;
        if (gameObject.layer == LayerMask.NameToLayer("Dead")) proj.GetComponent<SpriteRenderer>().material = GameManager.instance.gameData.DeadMaterial;
        foreach (Character character in GetComponent<Character>().player.characters)
        {
            Physics2D.IgnoreCollision(proj.GetComponent<Collider2D>(), character.GetComponent<Collider2D>());
        }
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
