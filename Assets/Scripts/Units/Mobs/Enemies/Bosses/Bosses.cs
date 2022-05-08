using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosses : Enemies
{
    protected override void Update()
    {   
        base.Update();
        Move(); 
    }
    public override void Move()
    {
        if(isMoveable == false)
        {
            return;
        }
        if(target != null )//&& mobState == State.Normal)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
        }
        else if((target == null) && (transform.position != spawnPosition))
        {
            Vector3 dir = (spawnPosition - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
        }
    }
}
