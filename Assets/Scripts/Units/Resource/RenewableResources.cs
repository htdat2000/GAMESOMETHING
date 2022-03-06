using UnityEngine;

public class RenewableResources : Resource
{
    protected int defaultHP;
    [SerializeField] protected Sprite havingMaterialImg;
    [SerializeField] protected Sprite notHavingMaterialImg;
    protected int renewTime;
    protected float cooldown; 
    protected SpriteRenderer spriteRenderer;  
    protected override async void Start()
    {   
        base.Start();
        defaultHP = hp;
        cooldown = renewTime;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
                
        InvokeRepeating("UpdateResourceStatus", 0, 0.5f);
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
    protected virtual async void GainMaterials()
    {
        for(int i = 0; i<currentMaterialHolding.Length; i++)
        {
            if(currentMaterialHolding[i] < maxMaterialCanHold[i])
            {
                if(cooldown <= 0)
                {
                    currentMaterialHolding[i]++;
                    cooldown = renewTime;
                }
                else
                    cooldown -= Time.deltaTime; 
            }
        }
    }
    protected virtual void UpdateResourceStatus()
    {
        if(currentMaterialHolding != null)
        {
            if(currentMaterialHolding[0] != null && currentMaterialHolding[0] <= 0) //material thứ 0 là main
            {
                spriteRenderer.sprite = notHavingMaterialImg;
            }
            else spriteRenderer.sprite = havingMaterialImg;
        }
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
        // for(int i = 0; i<currentMaterialHolding.Length; i++)
        // {
        //     if(currentMaterialHolding[i] > 0)
        //     {
        //         hp -= dmg;
        //         HPEqual0();
        //     }
        // }
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
