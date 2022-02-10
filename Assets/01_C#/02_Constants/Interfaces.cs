using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    BaseStats baseStats { get;}
    float currentHealth { get;}

    void TakeDamage(float damage);
}

public interface IBasicAttacks
{
    void BasicAttack();
}

public interface IUsable
{
    void Use();
}

public interface IInteractable
{
    void Interact();
}
