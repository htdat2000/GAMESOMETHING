using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creatures : DamageableObjects
{
    [SerializeField] protected float speed;
    [SerializeField] protected GameObject deadEffect;
    [SerializeField] protected SFX sfx;
    protected bool isFacingRight;

    public abstract void Move();
    protected abstract void Die();
    public abstract void Attack();
    protected void PlaySFX(SFX.SFXState state)
    {
        if(sfx != null)
        {
            sfx.PlaySFX(state);
        }
    }
}
