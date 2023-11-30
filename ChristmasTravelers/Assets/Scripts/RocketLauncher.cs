using BoardCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roquette : BasicAttack
{
    [SerializeField, Range(0, 1)] float directDamage;
    [SerializeField, Range(0, 1)] float indirectDamage;
    [SerializeField] private float explosionRadius;
    [SerializeField] ParticleSystem explosionEffect;
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
        if (proj.gameObject.layer == LayerMask.NameToLayer("Dead")) return;

        ParticleSystem effect = GameObject.Instantiate(explosionEffect);
        effect.transform.position = proj.transform.position;

        Collider2D[] casualties = Physics2D.OverlapCircleAll(proj.transform.position, explosionRadius);
        foreach (Collider2D c in casualties)
        {
            if (c.gameObject.layer == proj.gameObject.layer && c.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                StartCoroutine(DamageFeedBack(c.GetComponent<SpriteRenderer>()));
                damageable.Damage(atk * indirectDamage);
            }
        }
        Destroy(effect, 3);

        //DEBUG
        Debug.DrawCircle(proj.transform.position, explosionRadius, 20, Color.green);
        //END DEBUG
    }




    [Header("Damage feedback")]
    [SerializeField] private Color color;
    [SerializeField] private float time;
    private IEnumerator DamageFeedBack(SpriteRenderer sr)
    {
        Color colorRef = sr.color;
        sr.color = color;
        yield return new WaitForSeconds(time);
        sr.color = colorRef;
    }


}
