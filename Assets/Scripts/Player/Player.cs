﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creatures
{
    public float attackRange = 0f;
    float saveInput;
    Bag bag;
    Animator anim;
    
    void Start()
    {
        bag = GetComponent<Bag>();
        anim = GetComponent<Animator>();
    }

    #region Basic Function
    override public void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
        if(moveInput.x!=0)
            saveInput = moveInput.x;
        transform.position += (Vector3)moveAmount;
        MoveAnimationUpdate(moveInput);
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
    
    void MoveAnimationUpdate(Vector2 moveInput)
    {
        if(moveInput == Vector2.zero)
        {
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("IsRunning", true);
        }
    }
}
