using Records;
using System;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(CharacterAnimator))]
public class Character : MonoBehaviour {

    [field: SerializeField] public Team.Characters type { get; private set; }

    public Player player;

	public Team team;

	public Replay replay { get; private set; }
	public IRecorder[] recorders { get; private set; }

	[Header("Gameplay parameters")]
    public float grabRadius;
    [field : SerializeField] public int FOV { get; private set; }

    [field: SerializeField] public CharacterAnimator chAnimator{ get; private set; }

    [field: SerializeField]  public HealthBar healthBar { get; private set; }


    private Rigidbody2D body;



    private void Awake () {
		replay = GetComponent<Replay> ();
		recorders = GetComponents<IRecorder> ();
		body = GetComponent<Rigidbody2D> ();
		healthBar.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
	}

	public void Prepare(){
		transform.position = player.spawn;
		chAnimator.NotifyFlagDrop(this); //Initialize to default sprite library
	}


    public void UpdatePosition(Vector3 movement)
    {
        body.position += new Vector2(movement.x,movement.y);
		chAnimator.NotifyMovement(movement);
    }


	public void OnFlagGrabbed(){
		chAnimator.NotifyFlagGrabbed(this);
		Debug.Log("A toi l'animator");
	}

	public void OnFlagDropped(){
		chAnimator.NotifyFlagDrop(this);
	}


	public Sprite GetDisplaySprite(string option)
	{
		Sprite sprite = GetSpriteLibrary().GetSprite("UI", option.ToLower());
		if (!sprite)
		{
			Debug.LogError("Cannot find sprite for category : UI and label " + option + " in current sprite library");
			return null;
		}
		else
		{
			return sprite;
		}
	}



	public SpriteLibraryAsset GetSpriteLibrary()
	{
        team.libraries.TryGetValue(type, out CharacterLibrary library);
        return library.spriteLibrary;
    }

    public SpriteLibraryAsset GetChickenSpriteLibrary()
    {
        team.libraries.TryGetValue(type, out CharacterLibrary library);
        return library.chickenSpriteLibrary;
    }

}


[Serializable]
public struct CharacterLibrary
{
    public SpriteLibraryAsset spriteLibrary;
    public SpriteLibraryAsset chickenSpriteLibrary;
}
