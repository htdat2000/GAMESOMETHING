using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesHitBox : MonoBehaviour
{
    private Mobs thisMob;
    private int dmg;
    void Start() {
        thisMob = GetComponentInParent<Mobs>();
        dmg = thisMob.dmg;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {   
            Player player;
            collision.TryGetComponent<Player>(out player);
            player.TakeDmg(dmg);
            player.KnockbackEffect(this.gameObject);
        }
        if(collision.CompareTag("OtherDamageableByEnemies"))
        {
            DamageableObjects attackedObject;
            collision.TryGetComponent<DamageableObjects>(out attackedObject);
            attackedObject.TakeDmg(dmg);
        }
    }
}
