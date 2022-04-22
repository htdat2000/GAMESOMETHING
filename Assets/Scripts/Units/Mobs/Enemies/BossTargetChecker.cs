using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTargetChecker : TargetChecker
{
    protected Mobs boss;

    protected override void Start()
    {
        boss = GetComponentInParent<Mobs>();
    }
    protected override void  OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            boss.target = col.gameObject;
        }
    }
    protected override void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            boss.target = null;
        }
    }
}
