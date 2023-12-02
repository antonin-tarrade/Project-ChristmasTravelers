using BoardCommands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IAttack
{ 
    void Shoot();
    Vector2 shootDirection { get; set; }    
    bool IsCooldownReady();
    ShootCommand GenerateShootCommand();
    AimCommand GenerateAimCommand(Vector3 direction);
}

public interface IDamageable
{
    void Damage(float dmg);
}

