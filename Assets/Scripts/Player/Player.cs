using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : Creatures
{
    public float attackRange = 0f;

    [Header("Unity Components")]
    private Bag bag;
    Animator anim;
    private Rigidbody2D rb;

    [Header("Unity Script Varibles")]
    [HideInInspector]public Vector2 moveDir;
    float saveInput;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bag = GetComponent<Bag>();
        anim = GetComponent<Animator>();
    }
    void FixedUpdate() 
    {
        Move();
    }
    #region Basic Function
    override public void Move()
    {
        if(moveDir.x != 0)
            saveInput = moveDir.x;
        rb.velocity = moveDir * speed;
        MoveAnimationUpdate(moveDir);
    }
    override protected void Die()
    {
        return;
    }
    override public void TakeDmg(int dmg)
    {
        return;
    }
    override protected void HPEqual0() 
    {
        return;
    }
    override public void Attack()
    {
        // Vector2 attackPoint = new Vector2(transform.position.x, transform.position.y);
        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint, attackRange);
        // foreach(Collider2D enemy in hitEnemies){
        //     Debug.Log("Hit");
        // }

        if (saveInput>0){
            Debug.Log("Right");
        }
        if (saveInput<0){
            Debug.Log("Left");
        }
    }
    #endregion
    
    #region Animation
    void MoveAnimationUpdate(Vector2 _moveDir)
    {
        if(_moveDir == Vector2.zero)
        {
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("IsRunning", true);
        }
    }
    #endregion
}

