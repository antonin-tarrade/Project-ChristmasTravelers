using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damage(float dmg);
}


// Possibilité pour un système plus complet :

//public interface IDamageable
//{
//    void Damage(float damage);
//    // Visiteur 2
//    void AcceptDamage(IDamager damager, float damage);
//}

//public interface IDamager
//{
//    // Visiteur 1
//    void RequestDamage(IDamageable damageable, float damage)
//    {
//        damageable.AcceptDamage(this, damage);
//    }

//    void InflictDamage(IDamageable damageable, float damage);
//}
