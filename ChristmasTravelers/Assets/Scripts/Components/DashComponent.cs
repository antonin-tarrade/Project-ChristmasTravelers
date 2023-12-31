using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using System.Runtime.CompilerServices;
using System;
using UnityEngine.Events;
using static UnityEngine.ParticleSystem;

public class DashComponent : MonoBehaviour
{

    [SerializeField] private ParticleSystem particles;
    private EmissionModule emission;

    private void Start()
    {
        emission = particles.emission;
        emission.enabled = false;
    }


    public void Dash(float dashSpeed, float distance, Vector3 direction){
        StartCoroutine(ApplyDash(dashSpeed, distance, direction));
    }

    
    public IEnumerator ApplyDash(float acceleration, float distance, Vector3 direction) {
        emission.enabled = true;
        float dist = 0;
        float speed = 0;
        while (dist < distance)
        {
            speed += acceleration * 60 * Time.deltaTime;
            dist += speed * Time.deltaTime;
            transform.position += speed * Time.deltaTime * direction;
            yield return null;
        }
        emission.enabled = false;
    }

    
}




