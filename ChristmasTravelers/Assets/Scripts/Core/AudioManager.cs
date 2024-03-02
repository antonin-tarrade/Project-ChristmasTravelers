using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioClip characterDeathSFX;
    [SerializeField] private AudioClip characterDamageSFX;
    [SerializeField] private AudioClip itemCollectedSFX;
    [SerializeField] private AudioClip itemDeliveredSFX;
    [SerializeField] private AudioClip explosionSFX;

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



public static class AudioSourceExtensions
{
    public static void PlayAtPoint(this AudioClip clip, Vector3 point)
    {
        AudioSource.PlayClipAtPoint(clip, point);
    }

    public static void PlayAtPoint(this AudioClip clip, Transform t)
    {
        clip.PlayAtPoint(t.position);
    }

    public static void PlayAtPoint(this AudioClip clip, MonoBehaviour mb)
    {
        clip.PlayAtPoint(mb.transform);
    }
}
