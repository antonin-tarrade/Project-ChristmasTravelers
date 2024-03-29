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

	public bool isLinked;

	private IngameUIManager UI;

    private Rigidbody2D body;


    private void Awake () {
		isLinked = false;
		replay = GetComponent<Replay> ();
		recorders = GetComponents<IRecorder> ();
		body = GetComponent<Rigidbody2D> ();
		healthBar.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
		UI = IngameUIManager.instance;
	}

	public void Prepare(){
		transform.position = player.spawn;
		chAnimator.NotifyFlagDrop(this); //Initialize to default sprite library
	}


    public void UpdatePosition(Vector2 position)
    {
		Vector2 movement = position - body.position;
        
		chAnimator.NotifyMovement(movement);
        body.position = position;
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

	public void NotifyDamage(float dmg){
		healthBar.Change(-dmg);
		if (isLinked) UI.NotifyDamage(dmg);
	}

	public void InitHealthBars(float baseHealth){
		healthBar.InitBar(baseHealth);
		if (isLinked) UI.InitHealthBar(baseHealth);
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
