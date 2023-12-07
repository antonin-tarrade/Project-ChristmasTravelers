using BoardCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SniperAttack : BasicAttack
{
    [Header("Laser feedback parameters")]
    [SerializeField] LineRenderer laser;
    [SerializeField] Gradient chargeColorGradient;

    [Header("Shoot feedback")]
    [SerializeField] private ParticleSystem smokeParticlesPrefab;
    [SerializeField] private ParticleSystem impactParticlesPrefab;


    [Header("Attack parameters")]
    [SerializeField] private float chargeTime;
    [SerializeField] private float range;
    private bool isCharging;

    protected new void Awake()
    {
        base.Awake();
        SetLaserColor(chargeColorGradient.Evaluate(1));
    }


    public override IBoardCommand GenerateShootCommand()
    {
        if (base.CanShoot() && !isCharging) return new ShootCommand(this);
        else return null;
    }

    public override void Shoot()
    {
        Transform prefabBin = GameObject.Find("PrefabTrashBin").transform;

        ParticleSystem smokeParticles = Instantiate(smokeParticlesPrefab, transform.position + new Vector3(shootDirection.x, shootDirection.y).normalized, Quaternion.identity);
        smokeParticles.transform.SetParent(prefabBin);
        smokeParticles.transform.forward = shootDirection;
        Destroy(smokeParticles.gameObject, 5);
        Vector3 offset = shootDirection.normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, shootDirection * range);
        if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable) && damageable.gameObject.layer == LayerMask.NameToLayer("Alive"))
        {
            damageable.Damage(atk);
            ParticleSystem hitParticules = Instantiate(impactParticlesPrefab, damageable.gameObject.transform.position, Quaternion.identity);
            hitParticules.transform.SetParent(prefabBin);
            Destroy(hitParticules.gameObject, 5);
        }
        Charge();   
    }

    private void Charge()
    {
        StartCoroutine(RCharge());
    }

    private IEnumerator RCharge()
    {
        float time = 0;
        isCharging = true;
        while (time < chargeTime)
        {
            float ratio = time / chargeTime;
            SetLaserColor(chargeColorGradient.Evaluate(ratio));

            time += Time.deltaTime;
            yield return null;
        }
        SetLaserColor(chargeColorGradient.Evaluate(1));
        isCharging = false;  
    }


    private void SetLaserColor(Color color)
    {
        laser.startColor = color;
        laser.endColor = color;
    }


    private void PreviewShot()
    {
        Vector3 offset = shootDirection.normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, shootDirection * range);
        if (hit.collider != null)
        {
            Vector3 temp = hit.point;
            laser.SetPosition(1, temp - transform.position);
        }
        else
        {
            Vector3 temp = range * shootDirection.normalized;
            laser.SetPosition(1, temp - transform.position);
        }
    }


    private new void Update()
    {
        base.Update();
        PreviewShot();
    }



}
