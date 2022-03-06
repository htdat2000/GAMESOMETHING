using UnityEngine;

public class NonrenewableResources : Resource 
{
    protected int defaultHP;
    [SerializeField] protected Sprite havingMaterialImg;
    protected override void Start()
    {   
        base.Start();
        defaultHP = hp;
    }
    protected override void SpawnMaterials()
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
        // if(currentMaterialHoldinghp <= 0)
        // {
        //     Destroy(gameObject);
        // }
    }
    protected void SpawnItem(Items item)
    {
        itemPrototype.GetComponent<ItemPrototype>().item = item;
        Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
        Instantiate(itemPrototype, spawnPosition, Quaternion.identity);
    }
    public override void Remove()
    {
        return;
    }
    public override void Spawn()
    {
        return;
    }
    public override void TakeDmg(int dmg)
    {
        // hp -= dmg;
        // HPEqual0();
        SpawnMaterials();
    }
    protected override void HPEqual0()
    {
        if(hp <= 0)
        {
            SpawnMaterials();
            hp = defaultHP;
        }
    }
}
