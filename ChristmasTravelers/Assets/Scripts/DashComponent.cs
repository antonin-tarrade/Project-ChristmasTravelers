using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class DashComponent : MonoBehaviour
{


    public void Dash(float dashSpeed, float distance){
        StartCoroutine(ApplyDash(dashSpeed, distance));
    }


    public IEnumerator ApplyDash(float dashSpeed, float distance) {
        Vector3 finalPosition = transform.position + transform.right * distance;
        transform.position = finalPosition;
        yield return null;
    }

    
}




