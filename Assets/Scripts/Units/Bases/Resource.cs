using UnityEngine;

public abstract class Resource : DamageableObjects, IAutoSpawn
{
    [Header("Resource Info")]
    [SerializeField]protected int[] maxMaterialCanHold;
    protected int[] currentMaterialHolding;

    [Header("Unity Setup")]
    protected GameObject itemPrototype;
    [SerializeField] protected ItemDrop[] itemDrops;
    [SerializeField] protected SFX sfx;
    private Animator anim;

    protected virtual void Start()
    {
        currentMaterialHolding = new int[itemDrops.Length];
        SetupMaterial();
        itemPrototype = UnityEngine.Resources.Load<GameObject>("Prefabs/Items/ItemPrototype");
        anim = GetComponent<Animator>();
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
    protected virtual void SpawnMaterials()
    {   
        int randomValue = Random.Range(1, 1001);
        if(itemDrops.Length <= 0)
        {
            return;
        }

        int numberOfItem = itemDrops.Length;
        for (int i = 0; i <= numberOfItem - 1; i++)
        {
            if(itemDrops[i].SpawnItemByDropRate(randomValue) && currentMaterialHolding[i] >= 1)
            {
                SpawnItem(itemDrops[i].item, i);
            }
        }
        anim.Play("Attacked");
    }
    protected virtual void SpawnItem(Items item, int itemIndex)
    {
        currentMaterialHolding[itemIndex]--;
        itemPrototype.GetComponent<ItemPrototype>().item = item;
        Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
        Instantiate(itemPrototype, spawnPosition, Quaternion.identity);
    }
    protected void PlaySFX(SFX.SFXState state)
    {
        if(sfx != null)
        {
            sfx.PlaySFX(state);
        }
    }
    
}
