using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public int dmg;
    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageableObjects objectToAttack; 
        collision.TryGetComponent<DamageableObjects>(out objectToAttack);
        if(objectToAttack != null)
        {
            if(objectToAttack.gameObject.CompareTag("Enemies"))
            {
                objectToAttack.TakeDmg(dmg);
            }
        }
    }
}
