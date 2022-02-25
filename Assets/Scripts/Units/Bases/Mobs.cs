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
    protected GameObject target;
    protected GameObject itemPrototype;

    protected virtual void Start()
    {
        itemPrototype = UnityEngine.Resources.Load<GameObject>("Prefabs/Items/ItemPrototype");
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
    protected void DropItem()
    {
        int randomValue = Random.Range(1, 1001);
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
}
