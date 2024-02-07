using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTrackingProjectile : Projectile
{
    [SerializeField, Range(0, 1)] float lerpingFactor; 
    [SerializeField] private float radiusDetection;

    private void Update()
    {
        Character target = FindClosestTarget();
        if (target != null) direction = Vector3.Lerp(direction, (target.transform.position - transform.position).normalized, lerpingFactor);
        Move();
    }

    private Character FindClosestTarget()
    {
        Character closestTarget = null;
        float closestDistance = float.MaxValue;


        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, radiusDetection);
        foreach (Collider2D t in targets)
        {
            if (t.TryGetComponent<Character>(out Character target) 
                && target.player != character.player)
            {
                float dist = Vector3.Distance(transform.position, target.transform.position);
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closestTarget = target;
                }
            }
        }

        return closestTarget;
    }
}
