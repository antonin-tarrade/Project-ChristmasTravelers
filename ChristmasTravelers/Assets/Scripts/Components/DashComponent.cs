using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using System.Runtime.CompilerServices;
using System;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;
using System.Linq;

public class DashComponent : MonoBehaviour
{

    [SerializeField] private ParticleSystem particles;
    private EmissionModule emission;
    [SerializeField] private float raycastDistanceThreshold;

    private void Start()
    {
        emission = particles.emission;
        emission.enabled = false;
    }


    public void Dash(float dashSpeed, float distance, Vector3 direction){
        StartCoroutine(ApplyDash(dashSpeed, distance, direction));
    }

    
    public IEnumerator ApplyDash(float acceleration, float distance, Vector3 direction) {
        GetComponent<SimpleInput>().isActive = false;
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        emission.enabled = true;
        float dist = 0;
        float speed = 0;
        while (dist < distance)
        {
            speed += acceleration * 60 * Time.deltaTime;
            dist += speed * Time.deltaTime;
            //transform.position += speed * Time.deltaTime * direction;
            //body.position += dist * (Vector2)direction;
            body.velocity = speed * direction;
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, 100 * body.velocity)
                .Where(h => h.collider.gameObject.layer == LayerMask.NameToLayer("Map")).ToArray();
            bool shouldBreak = false; ;
            foreach (RaycastHit2D hit in hits )
            {
                if (hit.distance < raycastDistanceThreshold)
                {
                    StopAllCoroutines();
                    shouldBreak = true;
                    break;
                }
            }
            if (shouldBreak) break;
            yield return null;
        }
        body.velocity = Vector2.zero;
        emission.enabled = false;
        GetComponent<SimpleInput>().isActive = true;
    }

    
}




