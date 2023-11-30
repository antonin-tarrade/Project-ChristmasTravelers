using BoardCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roquette : BasicAttack
{
    [SerializeField, Range(0, 1)] float directDamage;
    [SerializeField, Range(0, 1)] float indirectDamage;
    [SerializeField] private float explosionRadius;
    public override ShootCommand GenerateCommand(Vector3 direction)
    {
        return new ShootCommand(this, direction);
    }

    public override Projectile InitProj(Vector3 direction)
    {
        Projectile proj = base.InitProj(direction);
        proj.OnEnd += Explode;
        return proj;
    }

    public override void OnHit(GameObject obj)
    {
        if (obj.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(atk * directDamage);
        }
    }

    public virtual void Explode(Projectile proj)
    {
        Collider2D[] casualties = Physics2D.OverlapCircleAll(proj.transform.position, explosionRadius);
        foreach (Collider2D c in casualties)
        {
            if (c.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.Damage(atk * indirectDamage);
            }
        }
    }


}
