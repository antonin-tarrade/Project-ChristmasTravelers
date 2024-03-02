using BoardCommands;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IAttack
{ 
    void Shoot();
    Vector2 shootDirection { get; set; }    
    bool IsCooldownReady();
    IBoardCommand GenerateShootCommand();
    IBoardCommand GenerateAimCommand(Vector3 direction);
}

public interface IDamageable
{
    event Action OnDeath;
    event Action OnDamage;
    void Damage(float dmg);
    GameObject gameObject { get; }
}

