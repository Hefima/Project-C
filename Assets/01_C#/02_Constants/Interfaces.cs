using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
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
