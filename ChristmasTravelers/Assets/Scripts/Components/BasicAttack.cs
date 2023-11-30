using BoardCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicAttack : MonoBehaviour, IAttack
{
    [field: SerializeField] public float atk { get; private set; }
    [field: SerializeField] public float spd { get; private set; }
    [field: SerializeField] public float lifeLength { get; private set; }
    [SerializeField] protected Projectile projectile;
    [SerializeField] protected float cooldown;
    protected float lastTimeShoot;


    public virtual void Shoot(Vector3 direction)
    {
        lastTimeShoot = Time.time;
        Projectile proj = InitProj(direction);
        foreach (Character character in GetComponent<Character>().player.characters)
        {
            Physics2D.IgnoreCollision(proj.GetComponent<Collider2D>(), character.GetComponent<Collider2D>());
        }
        proj.Shoot(direction, spd, lifeLength);
    }

    public virtual Projectile InitProj(Vector3 direction)
    {
        Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.transform.position += direction.normalized;
        proj.OnHit += OnHit;
        proj.gameObject.layer = gameObject.layer;
        if (gameObject.layer == LayerMask.NameToLayer("Dead")) proj.GetComponent<SpriteRenderer>().material = GameManager.instance.gameData.DeadMaterial;
        return proj;
    }


    public virtual void OnHit(GameObject obj)
    {
        if (obj.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(atk);
        }
    }


    public virtual bool IsCooldownReady()
    {
        return (Time.time - lastTimeShoot) > cooldown;
    }


    public abstract ShootCommand GenerateCommand(Vector3 direction);
}
