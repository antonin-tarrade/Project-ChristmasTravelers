using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour, IAttack
{
    [field: SerializeField] public float atk { get; private set; }
    [field: SerializeField] public float spd { get; private set; }
    [field: SerializeField] public float lifeLength { get; private set; }
    [SerializeField] private Projectile projectile;
    [SerializeField] private float cooldown;
    private float lastTimeShoot;
    private Vector3 shootDirection;
    

    public void Shoot(Vector3 direction)
    {
        lastTimeShoot = Time.time;
        Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity);
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


    public bool IsCooldownReady()
    {
        return (Time.time - lastTimeShoot) > cooldown;
    }
}
