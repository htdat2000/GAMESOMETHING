using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    protected GameObject target;
    [SerializeField] protected float attackSpeed;
    protected float lastAttack;
    [SerializeField] protected float attackRange;
    protected CircleCollider2D cir2D;
    
    protected virtual void Start()
    {
        SetAttackRange();
    }

    protected virtual void SetAttackRange()
    {
        cir2D = GetComponent<CircleCollider2D>();
        cir2D.radius = attackRange;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        if(col.CompareTag("Player"))
        {
            target = col.gameObject;
            Attack();
            Debug.Log("Hit");
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("OtherDamageableByEnemies"))
        {
            target = null;
        }
    }

    protected virtual void Attack()
    {
        if (lastAttack + attackSpeed > Time.time)
        {
            return;
        }
        lastAttack = Time.time;
    }
}
