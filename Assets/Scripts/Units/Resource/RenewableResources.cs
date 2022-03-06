using UnityEngine;

public class RenewableResources : Resource
{
    protected int defaultHP;
    [SerializeField] protected Sprite havingMaterialImg;
    [SerializeField] protected Sprite notHavingMaterialImg;
    protected int renewTime;
    protected float cooldown; 
    protected SpriteRenderer spriteRenderer;  
    protected override void Start()
    {   
        base.Start();
        defaultHP = hp;
        cooldown = renewTime;
        currentMaterialHolding = maxMaterialCanHold;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
                
        InvokeRepeating("UpdateResourceStatus", 0, 0.5f);
        SpawnMaterials();
        SpawnMaterials();
    }
    void Update()
    {
        GainMaterials();
    }
    protected override void SpawnMaterials()
    {   

        // currentMaterialHolding--;
        int randomValue = Random.Range(1, 1001);
        Debug.Log("Drop");
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
        Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
        Instantiate(itemPrototype, spawnPosition, Quaternion.identity);
    }
    protected virtual void GainMaterials()
    {
        if(currentMaterialHolding < maxMaterialCanHold)
        {
            if(cooldown <= 0)
            {
                currentMaterialHolding++;
                cooldown = renewTime;
            }
            else
                cooldown -= Time.deltaTime; 
        }
    }
    protected virtual void UpdateResourceStatus()
    {
        if(currentMaterialHolding <= 0)
        {
            spriteRenderer.sprite = notHavingMaterialImg;
        }
        else spriteRenderer.sprite = havingMaterialImg;
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
        if(currentMaterialHolding > 0)
        {
        hp -= dmg;
        HPEqual0();
        }
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
