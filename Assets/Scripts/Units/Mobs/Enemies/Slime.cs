using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemies
{
    private Vector2 randomPoint;
    private PathMover pathMover;
    protected override void Start() 
    {
        base.Start();
        InvokeRepeating("ChoseRandomDir", 0f, 3f);
        pathMover = GetComponent<PathMover>();
    }
    private void Update() 
    {
        if(pathMover.GetChasing() == false)
        {
            Move();
        }
    }
    #region Override
    override public void Move()
    {
        transform.position += (transform.position - (Vector3)randomPoint).normalized * speed * Time.deltaTime;
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
        return;
    }

    #endregion
    protected void Chase()
    {
        pathMover.Chase();
    }
    protected void StopChasing()
    {
        pathMover.StopChasing();
    }
    private void ChoseRandomDir()
    {
        float randx = Random.Range(transform.position.x - 1f,transform.position.x + 1f);
        float randy = Random.Range(transform.position.y - 1f,transform.position.y + 1f);
        randomPoint = new Vector2(randx, randy);
    }

    private void OnCollisionEnter(Collision other) 
    {
        // TakeDmg(other.gameObject.GetComponent<DamageableObjects>().dmg);
    }
}
