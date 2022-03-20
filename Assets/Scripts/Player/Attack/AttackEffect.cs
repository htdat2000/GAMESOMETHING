using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public int dmg;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemiesHurtBox"))
        {
            Mobs mobsToAttack; 
            mobsToAttack = collision.GetComponentInParent<Mobs>();
            mobsToAttack.TakeDmg(dmg);
            mobsToAttack.KnockbackEffect(this.gameObject);
        }
        else if(collision.CompareTag("OtherDamageableByPlayer"))
        {
            DamageableObjects objectToAttack;
            collision.TryGetComponent<DamageableObjects>(out objectToAttack);
            objectToAttack.TakeDmg(dmg);
        }
    }
}
