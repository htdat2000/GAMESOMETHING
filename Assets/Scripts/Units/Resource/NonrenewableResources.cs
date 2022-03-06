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
