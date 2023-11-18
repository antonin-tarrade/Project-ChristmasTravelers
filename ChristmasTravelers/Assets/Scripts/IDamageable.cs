using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IAttack
{
    void Shoot(Vector3 direction);
}

public interface IDamageable
{
    void Damage(float dmg);
}

