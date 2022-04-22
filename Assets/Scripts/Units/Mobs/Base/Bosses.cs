using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosses : Enemies
{
    [SerializeField] protected Vector2 activePosition;
    
    protected override void Update()
    {   
        base.Update();
        
        Move(); 
    }
    public override void Move()
    {
        if(target != null )//&& mobState == State.Normal)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
        }
    }
    
    
}
