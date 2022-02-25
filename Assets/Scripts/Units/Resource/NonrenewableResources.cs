using UnityEngine;

public class NonrenewableResources : Resource 
{
    protected int defaultHP;
    [SerializeField] protected Sprite havingMaterialImg;
    override void Start()
    {   
        base.Start();
        defaultHP = hp;
        currentMaterialHolding = maxMaterialCanHold;
    }
    protected override void SpawnMaterials()
    {
        currentMaterialHolding--;
        Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
        Instantiate(itemPrototype, spawnPosition, Quaternion.identity);
        if(currentMaterialHolding <= 0)
        {
            Destroy(gameObject);
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
        hp -= dmg;
        HPEqual0();
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
