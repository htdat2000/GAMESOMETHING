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

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {   
            DamageableObjects player;
            collision.TryGetComponent<DamageableObjects>(out player);
            player.TakeDmg(dmg);
            Attack();
            //do something to knockback player
        }
        if(collision.CompareTag("OtherDamageableByEnemies"))
        {
            Attack();
        }

    }
}
