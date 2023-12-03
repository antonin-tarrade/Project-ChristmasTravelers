using BoardCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayAttack : BasicAttack
{

    [Header("Gaussian spray parameters")]
    [SerializeField] private float amplitude;
    [SerializeField] private float offset;
    [SerializeField] private float sigma;
    [SerializeField] private float mu;
    [SerializeField, Range(0, 90)] private float sprayAngle;
    [SerializeField] private float accuracy;


    public override void Shoot()
    {
        float precision = amplitude * Helper.Gaussian(sigma, mu, Random.value * accuracy) + offset;
        float angle = Random.Range(-0.5f, 0.5f) * sprayAngle * precision;
        Vector3 dir = Helper.Rotate(shootDirection.normalized, angle * Mathf.Deg2Rad);
        Vector3 temp = shootDirection;
        shootDirection = dir;
        base.Shoot();
        shootDirection = temp;
    }
}
