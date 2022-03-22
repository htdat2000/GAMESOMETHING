using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemies
{
    private Vector2 randomPoint;
    protected override void Update() 
    {
        base.Update();
        Move();
    }
    override public void Move()
    {
        transform.position += (transform.position - (Vector3)randomPoint).normalized * speed * 0.1f * Time.deltaTime;
        SetAnim("Idle");
    }
}
