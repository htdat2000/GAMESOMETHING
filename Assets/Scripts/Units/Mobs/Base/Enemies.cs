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
        if(collision.gameObject.CompareTag("Player"))
        {   
            Player player;
            collision.gameObject.TryGetComponent<Player>(out player);
            player.TakeDmg(dmg);
            player.KnockbackEffect(this.gameObject);
        }
        if(collision.gameObject.CompareTag("OtherDamageableByEnemies"))
        {
            DamageableObjects attackedObject;
            collision.gameObject.TryGetComponent<DamageableObjects>(out attackedObject);
            attackedObject.TakeDmg(dmg);
        }
    }
}
