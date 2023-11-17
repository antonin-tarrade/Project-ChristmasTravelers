using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dash Settings")]
    
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ApplyDash(Character character){

        float startTime = Time.time;
        float endTime = startTime + dashTime;

        while (Time.time < endTime){
            character.transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
