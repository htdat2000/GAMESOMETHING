using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    protected GameObject target;
    [SerializeField] protected float attackSpeed;
    protected float lastAttack = 0;
    [SerializeField] protected float attackRange;

    [Header("Skill Cooldown")]
    [SerializeField] protected float skillCooldown;
    protected float lastSkillAttack = 0;

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

    protected virtual void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player") || col.CompareTag("OtherDamageableByEnemies"))
        {
            target = col.gameObject;
            CheckIsAttackable();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("OtherDamageableByEnemies"))
        {
            target = null;
        }
    }

    protected virtual void CheckIsAttackable()
    {
        if (lastAttack + attackSpeed < Time.time)
        {
            Attack();
            lastAttack = Time.time;
            return;
        }
        if (lastSkillAttack + skillCooldown < Time.time)
        {
            BossSkill();
            lastSkillAttack = Time.time;
            return;
        }
    }

    protected virtual void Attack()
    {
        
    }

    protected virtual void BossSkill()
    {
        
    }   
}
