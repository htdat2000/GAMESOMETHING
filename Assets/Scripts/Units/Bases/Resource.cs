using UnityEngine;

public abstract class Resource : DamageableObjects, IAutoSpawn
{
    [Header("Resource Info")]
    [SerializeField]protected int[] maxMaterialCanHold;
    protected int[] currentMaterialHolding;

    [Header("Unity Setup")]
    protected GameObject itemPrototype;
    [SerializeField] protected ItemDrop[] itemDrops;

    protected virtual void Start()
    {
        currentMaterialHolding = new int[itemDrops.Length];
        SetupMaterial();
        itemPrototype = UnityEngine.Resources.Load<GameObject>("Prefabs/Items/ItemPrototype");
    }
    // protected abstract void SpawnMaterials();
    public abstract void Remove();
    public abstract void Spawn();
    private void SetupMaterial()
    {
        currentMaterialHolding = new int[maxMaterialCanHold.Length];
        for (int i = 0; i < maxMaterialCanHold.Length; i++)
        {
            currentMaterialHolding[i] = maxMaterialCanHold[i];
        }
    }
    protected void SpawnMaterials()
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
        Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
        Instantiate(itemPrototype, spawnPosition, Quaternion.identity);
    }
}
