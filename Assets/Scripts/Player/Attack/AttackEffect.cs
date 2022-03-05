using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public int dmg;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemies"))
        {
            Mobs mobsToAttack; 
            collision.TryGetComponent<Mobs>(out mobsToAttack);
            mobsToAttack.TakeDmg(dmg);
            mobsToAttack.KnockbackEffect(this.gameObject);
        }
    }
}
