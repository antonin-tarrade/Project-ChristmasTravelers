using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField] private VolumeSFX characterDeathSFX;
    [SerializeField] private VolumeSFX characterDamageSFX;
    [SerializeField] private VolumeSFX itemCollectedSFX;
    [SerializeField] private VolumeSFX itemDeliveredSFX;
    [SerializeField] private VolumeSFX explosionSFX;

    private GameManager gameManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.OnCharacterSpawned += SetUpAudio;
        PlayerSpawn.OnFlagCollected += (PlayerSpawn spawn) => itemDeliveredSFX.PlayAtPoint(spawn);
        GrabbableItem.OnItemGrabbed += (GrabbableItem item) => itemCollectedSFX.PlayAtPoint(item);
        Roquette.OnExplosion += (Vector3 position) => explosionSFX.PlayAtPoint(position);
    }

    private void SetUpAudio(Character character)
    {
        character.GetComponent<IDamageable>().OnDamage += () => characterDamageSFX.PlayAtPoint(character);
        character.GetComponent<IDamageable>().OnDeath += () => characterDeathSFX.PlayAtPoint(character);
    }

}

[Serializable]
public class VolumeSFX
{
    [field: SerializeField] private AudioClip clip;
    [field: SerializeField, Range(0, 1)] private float volume;

    public static implicit operator AudioClip(VolumeSFX sfx) => sfx.clip;
}



public static class AudioSourceExtensions
{
    public static void PlayAtPoint(this VolumeSFX clip, Vector3 point)
    {
        AudioSource.PlayClipAtPoint(clip, point);
    }

    public static void PlayAtPoint(this VolumeSFX clip, Transform t)
    {
        clip.PlayAtPoint(t.position);
    }

    public static void PlayAtPoint(this VolumeSFX clip, MonoBehaviour mb)
    {
        clip.PlayAtPoint(mb.transform);
    }
}
