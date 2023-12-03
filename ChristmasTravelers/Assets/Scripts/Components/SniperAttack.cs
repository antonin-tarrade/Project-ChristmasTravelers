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


    [Header("Attack parameters")]
    [SerializeField] private float chargeTime;
    [SerializeField] private float range;
    private bool isCharging;

    private void Awake()
    {
        SetLaserColor(chargeColorGradient.Evaluate(1));
    }


    public override IBoardCommand GenerateShootCommand()
    {
        if (base.CanShoot() && !isCharging) return new ShootCommand(this);
        else return null;
    }

    public override void Shoot()
    {
        Vector3 offset = shootDirection.normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, shootDirection * range);
        if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(atk);
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
        Debug.DrawRay(transform.position + offset, shootDirection * range);
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


    private void Update()
    {
        PreviewShot();
    }



}
