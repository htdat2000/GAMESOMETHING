using UnityEngine;

public abstract class Resource : DamageableObjects, IAutoSpawn
{
    [Header("Resource Info")]
    [SerializeField]protected int maxMaterialCanHold;
    protected int currentMaterialHolding;
    [SerializeField]protected Items materialsHolding;

    [Header("Unity Setup")]
    protected GameObject itemPrototype;

    protected void Start()
    {
        itemPrototype = UnityEngine.Resources.Load<GameObject>("Prefabs/Items/ItemPrototype");
        itemPrototype.GetComponent<ItemPrototype>().item = materialsHolding;
    }
    protected abstract void SpawnMaterials();
    public abstract void Remove();
    public abstract void Spawn();
}
