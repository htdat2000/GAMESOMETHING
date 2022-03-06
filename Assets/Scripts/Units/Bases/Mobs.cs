﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
// public class ItemDrop
// {
//     [SerializeField] private Items item; public Items Item { get {return Item;} }
//     [SerializeField] private int dropRate; //percentage 

//     public bool SpawnItemByDropRate(int randomValue)
//     {
//         if(randomValue <= dropRate * 10)
//         {
//             return true;
//         }
//         else 
//         {
//             return false;
//         }        
//     }
// }

public abstract class Mobs : Creatures, IAutoSpawn
{
    [SerializeField] protected ItemDrop[] itemDrops;
    [SerializeField] protected int defaultHP;
    [SerializeField] protected int defaultDmg;
    protected GameObject target;
    protected GameObject itemPrototype;
    private Vector3 spawnPosition;
    [SerializeField] private float activeRadius = 50f;
    Rigidbody2D rigid2D;
    private float lastAttackedTime;
    [SerializeField] float knockbackTime;

    protected enum State
    {
        Normal,
        Attacked
    }
    protected State mobState = State.Normal; 
    
    protected virtual void Start()
    {
        spawnPosition = transform.position;
        itemPrototype = UnityEngine.Resources.Load<GameObject>("Prefabs/Items/ItemPrototype");
        LoadParameter();
        LoadComponent();
    }
    protected virtual void Update()
    {
        if(Vector3.Distance(spawnPosition, transform.position) > activeRadius)
        {
            transform.position = spawnPosition;
            Debug.Log("back to spawn pos");
        }
        if (lastAttackedTime + knockbackTime <= Time.time)
        {
            KnockBackOff();
        }
    }
    public void Remove()
    {
        return;
    }
    public void Spawn()
    {
        return;
    }
    protected void UpdateTarget()
    {
        return;
    }

    public override void TakeDmg(int dmg)
    {
        if(mobState == State.Normal)
        {
            mobState = State.Attacked;
            Debug.Log("Take dmg and change to Attacked state: " + mobState);
            // StartCoroutine(ResetMobState());
            KnockbackEffect();
            hp -= dmg;
            hp = Mathf.Clamp(hp, 0, defaultHP);
            HPEqual0();
        } 
    }

    protected override void HPEqual0()
    {
        if(hp <= 0)
        {
            Die();
        }  
    }

    protected override void Die()
    {
        DropItem();
        Destroy(gameObject);
    }

    protected virtual void DropItem()
    {
        int randomValue = Random.Range(1, 1001);
        if(itemDrops.Length <= 0)
        {
            return;
        }
        foreach (ItemDrop item in itemDrops)
        {
            if(item.SpawnItemByDropRate(randomValue))
            {
                SpawnItem(item.Item);
            }
        }
    }

    protected virtual void SpawnItem(Items item)
    {
        itemPrototype.GetComponent<ItemPrototype>().item = item;
        Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
        Instantiate(itemPrototype, spawnPosition, Quaternion.identity);
    }

    protected virtual void LoadParameter()
    {
        hp = defaultHP;
        dmg = defaultDmg;
        lastAttackedTime = Time.time;
    }

    protected virtual void LoadComponent()
    {
        TryGetComponent<Rigidbody2D>(out rigid2D);
    }
    public virtual void KnockbackEffect()
    {   
        if(rigid2D != null && mobState == State.Attacked)
        {
            lastAttackedTime = Time.time;
            Debug.Log("Knockback");
            Vector3 direction = new Vector3 (0, 3, 0);
            rigid2D.velocity = direction;
        }
    }
    public virtual void KnockBackOff()
    {
        mobState = State.Normal;
        rigid2D.velocity = Vector3.zero;
        Debug.Log("My velocity: " + rigid2D.velocity);
    }
    protected IEnumerator ResetMobState()
    {
        mobState = State.Normal;
        yield return new WaitForSeconds(2f);
    } 
}
