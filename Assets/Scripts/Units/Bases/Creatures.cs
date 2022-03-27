using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creatures : DamageableObjects
{
    [SerializeField] protected float speed;
    [SerializeField] protected GameObject deadEffect;
    protected bool isFacingRight;

    public abstract void Move();
    protected abstract void Die();
    public abstract void Attack();
}
