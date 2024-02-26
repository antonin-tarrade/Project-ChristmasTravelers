using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteDuplicator : MonoBehaviour
{

    public static SpriteDuplicator Duplicate(SpriteRenderer spriteRenderer)
    {
        SpriteDuplicator duplicate = new GameObject().AddComponent<SpriteDuplicator>();
        duplicate.name = spriteRenderer.gameObject.name + " duplicate";
        duplicate.transform.SetParent(spriteRenderer.transform);
        duplicate.origin = spriteRenderer;
        Debug.Log("Duplicate creation");
        return duplicate;
    }

    [SerializeField] private SpriteRenderer origin;
    private SpriteRenderer duplicate;


    private void Awake()
    {
        duplicate = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Minimap");
        duplicate.color = origin.color;
    }

    private void LateUpdate()
    {
        duplicate.sprite = origin.sprite;
    }
}
