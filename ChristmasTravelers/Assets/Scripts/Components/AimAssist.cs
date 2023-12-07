using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AimAssist 
{

    private Character character;
    private Transform characterTransform;

    [Header("Aiming parameters")]
    [SerializeField] private bool useAimAssist;
    [SerializeField] private float aimAssistAngle;
    [SerializeField, Range(0, 1)] private float aimAssistDamping;
    [SerializeField] private float aimAssistRange;
    // Le nombre de degrés par rayon lancé
    private static readonly float degPerRay = 0.5f;


    public void Set(Character character)
    {
        this.character = character;
        characterTransform = character.transform;
    }
    public Vector3 Assist(Vector3 direction)
    {
        direction = direction.normalized;
        // Aim assist detection
        for (float i = 0; i < aimAssistAngle; i += degPerRay)
        {
            RaycastHit2D hit = Physics2D.Raycast(characterTransform.position + direction, Helper.Rotate(direction * aimAssistRange, degPerRay * i * Mathf.Deg2Rad));
            RaycastHit2D hit2 = Physics2D.Raycast(characterTransform.position + direction, Helper.Rotate(direction * aimAssistRange, -degPerRay * i * Mathf.Deg2Rad));

            if (CustomLerp(direction, out Vector3 assistedDirection, hit)) return assistedDirection;
            if (CustomLerp(direction, out assistedDirection, hit2)) return assistedDirection;
        }
        return direction;
    }

    private bool CustomLerp(Vector3 direction, out Vector3 lerpedDirection, RaycastHit2D hit)
    {
        lerpedDirection = direction;
        if (hit.collider != null
                && hit.collider.TryGetComponent<Character>(out Character target)
                && target.player != character.player)
        {
            float aimAssistFactor = aimAssistDamping * (1 - Vector3.Angle(direction, target.transform.position - characterTransform.position) / aimAssistAngle);
            lerpedDirection = Vector3.Lerp(direction, target.transform.position - characterTransform.position, aimAssistFactor);
            return true;
        }
        else return false;
    }
}
