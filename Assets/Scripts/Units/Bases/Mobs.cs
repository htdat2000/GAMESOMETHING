using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDrop
{
    [SerializeField] private Items item; public Items Item { get {return Item;} }
    [SerializeField] private int dropRate; //percentage 

    public bool SpawnItemByDropRate(int randomValue)
    {
        if(randomValue <= dropRate * 10)
        {
            return true;
        }
        else 
        {
            return false;
        }        
    }
}

public abstract class Mobs : Creatures, IAutoSpawn
{
    [SerializeField] protected ItemDrop[] itemDrops;
    [SerializeField] protected int defaultHP;
    [SerializeField] protected int defaultDmg;
    protected GameObject target;
    protected GameObject itemPrototype;
    protected Rigidbody2D rigid2D;
    
    protected virtual void Start()
    {
        itemPrototype = UnityEngine.Resources.Load<GameObject>("Prefabs/Items/ItemPrototype");
        LoadParameter();
        LoadComponent();
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
        hp -= dmg;
        hp = Mathf.Clamp(hp, 0, defaultHP);
        HPEqual0();
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

    protected void DropItem()
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

    protected void SpawnItem(Items item)
    {
        itemPrototype.GetComponent<ItemPrototype>().item = item;
    }
    protected void LoadParameter()
    {
        hp = defaultHP;
        dmg = defaultDmg;
    }
    protected void LoadComponent()
    {
        TryGetComponent<Rigidbody2D>(out rigid2D);
    }
    public void KnockbackEffect(GameObject attacker)
    {   
        Vector2 direction = new Vector2(attacker.transform.position.x - this.gameObject.transform.position.x, attacker.transform.position.y - this.gameObject.transform.position.y);
        if(rigid2D != null)
        {
            rigid2D.AddForce(direction * 2, ForceMode2D.Impulse);
            StartCoroutine(SetPhysicsRigidbody2D());
        }
    }

    IEnumerator SetPhysicsRigidbody2D()
    {
        rigid2D.isKinematic = true;
        
        //rigid2D.bodyType = RigidbodyType2D.Kinematic;
        //rigid2D.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(1);
    }
}
