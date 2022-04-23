using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Mobs
{
    override public void Move()
    {
        return;
    }
    override public void Attack() 
    {
        return;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        //MeleeAttack(collision.gameObject);
    }

    protected virtual void MeleeAttack(GameObject target)
    {
        if(target.CompareTag("Player"))
        {   
            Player player;
            target.TryGetComponent<Player>(out player);
            player.TakeDmg(dmg);
            player.KnockbackEffect(this.gameObject);
        }
        if(target.CompareTag("OtherDamageableByEnemies"))
        {
            DamageableObjects attackedObject;
            target.TryGetComponent<DamageableObjects>(out attackedObject);
            attackedObject.TakeDmg(dmg);
        }
    }
}
