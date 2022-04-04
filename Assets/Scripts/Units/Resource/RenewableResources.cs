using UnityEngine;

public class RenewableResources : Resource
{
    protected int defaultHP;
    [SerializeField] protected Sprite havingMaterialImg;
    [SerializeField] protected Sprite notHavingMaterialImg;
    [SerializeField] protected int renewTime;
    protected float cooldown; 
    protected SpriteRenderer spriteRenderer;  
    protected override async void Start()
    {   
        base.Start();
        defaultHP = hp;
        cooldown = renewTime;
        
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
                
        InvokeRepeating("UpdateResourceStatus", 0, 0.5f);
    }
    void Update()
    {
        GainMaterials();
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
        sfx.PlaySFX(SFX.SFXState.HurtSFX);
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
