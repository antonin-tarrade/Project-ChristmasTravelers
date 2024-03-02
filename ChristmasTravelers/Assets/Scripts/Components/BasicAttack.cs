using BoardCommands;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.TextCore.Text;

public class BasicAttack : MonoBehaviour, IAttack
{
    [SerializeField] private AudioClip atkSFX;
    [field: SerializeField] public float atk { get; private set; }
    [field: SerializeField] public float spd { get; private set; }
    [field: SerializeField] public float lifeLength { get; private set; }
    [SerializeField] protected Projectile projectile;
    [SerializeField] protected float cooldown;
    protected float lastTimeShoot;
    public Vector2 shootDirection { get; set; } = Vector3.up;
    // The necessary angle difference to update aim direction
    [SerializeField] private float aimTriggerTreshold;
    [SerializeField] private AimAssist aimAssist;

    protected void Awake()
    {
        aimAssist.Set(GetComponent<Character>());
    }


    public virtual void Shoot()
    {
        lastTimeShoot = Time.time;
        Projectile proj = InitProj(shootDirection);
        foreach (Character character in GetComponent<Character>().player.characterInstances)
        {
            Physics2D.IgnoreCollision(proj.GetComponent<Collider2D>(), character.GetComponent<Collider2D>());
        }
        foreach (Collider2D collider in GetComponent<Character>().player.toAvoid)
        {
            Physics2D.IgnoreCollision(proj.GetComponent<Collider2D>(), collider);
        } 
        proj.Shoot(shootDirection, spd, lifeLength);
        AudioSource.PlayClipAtPoint(atkSFX, transform.position);
    }

    public virtual Projectile InitProj(Vector3 direction)
    {
        Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.transform.SetParent(GameObject.Find("PrefabTrashBin").transform);
        proj.SetCharacter(GetComponent<Character>());
        proj.transform.position += direction.normalized;
        proj.OnHit += OnHit;
        if (gameObject.layer == LayerMask.NameToLayer("Dead")) proj.gameObject.layer = gameObject.layer;
        proj.GetComponent<SpriteRenderer>().color = GetComponent<Character>().player.color;
        //if (gameObject.layer == LayerMask.NameToLayer("Dead")) proj.GetComponent<SpriteRenderer>().material = GameManager.instance.gameData.DeadMaterial;
        return proj;
    }


    public virtual void OnHit(GameObject obj)
    {
        if (obj.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(atk);
        }
    }


    public virtual bool IsCooldownReady()
    {
        return (Time.time - lastTimeShoot) > cooldown;
    }


    public virtual bool CanShoot()
    {
        return IsCooldownReady() && shootDirection.sqrMagnitude > 0;
    }
    public virtual IBoardCommand GenerateShootCommand()
    {
        if (CanShoot()) return new ShootCommand(this);
        else return null;
    }
    public virtual bool CanAim()
    {
        return true;
    }
    public virtual IBoardCommand GenerateAimCommand(Vector3 direction)
    {
        if (direction.sqrMagnitude > 0 && Vector2.Angle(direction, shootDirection) > aimTriggerTreshold && CanAim())
        {
            return new AimCommand(this, aimAssist.Assist(direction));
        }
        else return null;
    }

    protected void Update()
    {
        //shootDirection = aimAssist.Assist(shootDirection);
    }
}
