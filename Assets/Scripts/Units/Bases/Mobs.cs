using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mobs : Creatures, IAutoSpawn
{
    [SerializeField] protected ItemDrop[] itemDrops;
    [SerializeField] protected int defaultHP;
    [SerializeField] protected int defaultDmg;
    protected GameObject target;
    protected GameObject itemPrototype;
    protected Vector3 spawnPosition;
    [SerializeField] private float activeRadius = 50f;
    Rigidbody2D rigid2D;
    [Header("Const")]
    protected const float KNOCKBACK_TIME = 0.5f;
    protected const float ATTACKED_TIME = 1;

    [SerializeField] private Animator invAnim;
    [SerializeField] private Animator anim;
    [SerializeField] GameObject model;

    protected enum State
    {
        Normal,
        Attacked
    }
    protected State mobState = State.Normal; 
    protected virtual void Awake()
    {
        LoadParameter();
        LoadComponent();
    }
    protected virtual void Start()
    {
        spawnPosition = transform.position;
        itemPrototype = UnityEngine.Resources.Load<GameObject>("Prefabs/Items/ItemPrototype");
    }
    protected virtual void Update()
    {
        if(Vector3.Distance(spawnPosition, transform.position) > activeRadius)
        {
            BackToFirstSpawnPos();
        }
        FacingCheck();
    }
    protected virtual void BackToFirstSpawnPos()
    {
        transform.position = spawnPosition;
    }
    public bool IsState(string state)
    {
        switch (state)
        {
            case "Normal":
                return mobState == State.Normal;
                break;
            case "Attacked":
                return mobState == State.Attacked;
                break;
            default:
                return false;
                break;
        }
    }
    private void FacingCheck()
    {
        if(isFacingRight)
            model.transform.localScale = new Vector3(1f, 1f, 1f);
        else 
            model.transform.localScale = new Vector3(-1f, 1f, 1f);
    }
    public void Remove()
    {
        return;
    }
    public void Spawn()
    {
        return;
    }
    protected void UpdateTarget()
    {
        return;
    }

    public override void TakeDmg(int dmg)
    {
        if(mobState == State.Normal)
        {
            mobState = State.Attacked;
            StartCoroutine(AttackedOff());
            //KnockbackEffect();
            invAnim.Play("Invisible");

            hp -= dmg;
            hp = Mathf.Clamp(hp, 0, defaultHP);
            HPEqual0();
        } 
    }

    protected override void HPEqual0()
    {
        if(hp <= 0)
        {
            Die();
        }  
    }

    protected override void Die()
    {
        SpawnDeadEffect();
        DropItem();
        Destroy(gameObject);
    }

    protected virtual void SpawnDeadEffect()
    {
        Instantiate(deadEffect, transform.position, Quaternion.identity);;
    }

    protected virtual void DropItem()
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
                SpawnItem(item.item);
            }
        }
    }

    protected virtual void SpawnItem(Items item)
    {
        itemPrototype.GetComponent<ItemPrototype>().item = item;
        Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
        Instantiate(itemPrototype, spawnPosition, Quaternion.identity);
    }

    protected virtual void LoadParameter()
    {
        isFacingRight = true;
        hp = defaultHP;
        dmg = defaultDmg;
        GetComponentInChildren<EnemiesHitBox>().SetDmg(dmg);
    }

    protected virtual void LoadComponent()
    {
        TryGetComponent<Rigidbody2D>(out rigid2D);
    }

    public virtual void KnockbackEffect(GameObject attacker)
    {   
        if(rigid2D != null && mobState == State.Attacked)
        {
            StartCoroutine(KnockBackOff());
            Vector3 direction = this.gameObject.transform.position - attacker.transform.position;
            rigid2D.velocity = direction.normalized * 2;
        }
    }
    #region Turn Off Effect And State Method
    protected virtual IEnumerator AttackedOff()
    {
        yield return new WaitForSeconds(ATTACKED_TIME);
        mobState = State.Normal;
    }
    protected virtual IEnumerator KnockBackOff()
    {
        yield return new WaitForSeconds(KNOCKBACK_TIME); 
        rigid2D.velocity = Vector2.zero;
    }
    #endregion
    public void SetAnim(string animation)
    {
        anim.Play(animation);
    }
}
