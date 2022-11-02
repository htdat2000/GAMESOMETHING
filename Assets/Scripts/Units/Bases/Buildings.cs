using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buildings : DamageableObjects, IInteractables
{
    protected abstract void Broken();
    public virtual void Interact()
    {
        //Nếu là NPC: thực hiện A
        //Nếu là người chơi: thực hiện B
    }
}
