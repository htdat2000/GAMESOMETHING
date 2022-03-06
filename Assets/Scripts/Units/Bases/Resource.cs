using UnityEngine;

public abstract class Resource : DamageableObjects, IAutoSpawn
{
    [Header("Resource Info")]
    [SerializeField]protected int[] maxMaterialCanHold;
    protected int[] currentMaterialHolding;
    // [SerializeField]protected Items materialsHolding;

    [Header("Unity Setup")]
    protected GameObject itemPrototype;
    [SerializeField] protected ItemDrop[] itemDrops;

    protected virtual void Start()
    {
        currentMaterialHolding = new int[itemDrops.Length];
        SetupMaterial();
        itemPrototype = UnityEngine.Resources.Load<GameObject>("Prefabs/Items/ItemPrototype");
        // itemPrototype.GetComponent<ItemPrototype>().item = materialsHolding;
    }
    protected abstract void SpawnMaterials();
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
}
