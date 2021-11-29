using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(int damage);
}

public interface IBasicAttacks
{
    void BasicAttack();
}

public interface IUsable
{
    void Use();
}
