using UnityEngine;
using System.Linq;

public class NonrenewableResources : Resource 
{
    protected int defaultHP;
    [SerializeField] protected Sprite havingMaterialImg;
    protected bool[] isOutOfMaterials;
    protected override void Start()
    {   
        base.Start();
        defaultHP = hp;
        CreateData();
    }

    protected void CreateData()
    {
        int numberOfMaterialHolding = currentMaterialHolding.Length;
        isOutOfMaterials = new bool [numberOfMaterialHolding];
        for (int i = 0; i <= numberOfMaterialHolding - 1; i++)
        {
            isOutOfMaterials[i] = false;
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

    protected override void SpawnItem(Items item, int itemIndex)
    {
        base.SpawnItem(item, itemIndex);
        CheckAmountAfterSpawn(itemIndex);
    }

    protected override void SpawnMaterials()
    {
        base.SpawnMaterials();
        CheckIsOutOfAllMaterials();
    }

    protected void CheckAmountAfterSpawn(int itemIndex)
    {
        if(currentMaterialHolding[itemIndex] <= 0)
        {
            isOutOfMaterials[itemIndex] = true;
        }
    }

    protected void CheckIsOutOfAllMaterials()
    {
        if(isOutOfMaterials.All(value => value == true))
        {
            Destroy(gameObject);
        }
    }
}
