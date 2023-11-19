using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class DashItem : ItemConsumable
{
    [Header("Dash Settings")]
    
    float dashSpeed;
    float dashTime;
    private ItemData data;

    public DashItem(float dashSpeed, float dashTime){
        this.dashSpeed = dashSpeed;
        this.dashTime = dashTime;
    }

    override protected void OnUse(Character character){
        character.StartCoroutine(ApplyDash(character));
    }

    public IEnumerator ApplyDash(Character character)
    {
        // Store the initial position of the character
        Vector2 startPosition = character.transform.position;

        // Calculate the end position of the dash
        Vector2 endPosition = startPosition + (Vector2)character.transform.right * dashSpeed * dashTime;

        // // Disable character movement during the dash
        // character.CanMove = false;

        // Move the character towards the end position over the dash time
        float elapsedTime = 0f;
        while (elapsedTime < dashTime)
        {
            // Calculate the current position based on the elapsed time
            float t = elapsedTime / dashTime;
            Vector2 currentPosition = Vector2.Lerp(startPosition, endPosition, t);

            // Update the character's position
            character.transform.position = currentPosition;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // // Enable character movement after the dash
        // character.CanMove = true;
    }
}
