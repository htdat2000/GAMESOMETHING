using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemies
{
    private Vector2 randomPoint;
    
    protected override void Start() 
    {
        base.Start();
        InvokeRepeating("ChoseRandomDir", 0f, 3f);
    }
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
    private void ChoseRandomDir()
    {
        float randx = Random.Range(transform.position.x - 1f,transform.position.x + 1f);
        float randy = Random.Range(transform.position.y - 1f,transform.position.y + 1f);
        randomPoint = new Vector2(randx, randy);
    }
}
