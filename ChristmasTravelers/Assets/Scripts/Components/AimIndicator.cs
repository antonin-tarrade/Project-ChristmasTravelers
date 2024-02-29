using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimIndicator : MonoBehaviour
{
    [SerializeField] GameObject character;
    private Transform characterTransform;

    private IAttack characterAttack;

    [SerializeField] private float distance;
    Vector3 aimDirection;

    // Start is called before the first frame update
    void Start()
    {
        characterAttack = character.GetComponent<IAttack>();
        characterTransform = character.transform;
        GetComponent<SpriteRenderer>().color = character.GetComponent<Character>().player.team.teamColor;
    }

    // Update is called once per frame
    void Update()
    {
        aimDirection = characterAttack.shootDirection;
        transform.up = aimDirection;
        transform.position = characterTransform.position + distance * aimDirection.normalized;
    }
}
