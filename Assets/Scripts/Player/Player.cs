using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : Creatures
{
    public float attackRange = 0f;
    private float _hunger;
 
    [Header("Default Value")]
    private float defaultHunger = 100;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private int defaultHp;

    [Header("Unity Components")]
    private Bag bag;
    private Animator anim;
    private Rigidbody2D rb;

    [Header("Unity Script Variables")]
    [HideInInspector]public Vector2 moveDir;
    float saveInput;
    
    [Header("Hunger Function Variables")]
    private float hungerCooldown = 0;
    private float hungerTimer = 2;
    private bool isHunger = false;
    private float hungerDmgCooldown = 0;
    private float hungerDmgTimer = 1;
    
    
    #region Properties
    public float hunger 
    { 
        get 
        {
            return _hunger;
        } 
        set 
        {
            _hunger += value;
            _hunger = Mathf.Clamp(_hunger, 0, 100);
        } 
    }
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bag = GetComponent<Bag>();
        anim = GetComponent<Animator>();

        hp = defaultHp;
        hunger = defaultHunger;
        speed = defaultSpeed;
    }

    void Update()
    {
        Hunger();
        IsHunger();
    }

    void FixedUpdate() 
    {
        Move();
    }

    #region Basic Function
    override public void Move()
    {
        if(moveDir.x != 0)
            saveInput = moveDir.x;
        rb.velocity = moveDir * speed;
        MoveAnimationUpdate(moveDir);
    }
    override protected void Die()
    {
        return;
    }
    override public void TakeDmg(int dmg)
    {
        hp -= dmg;
        hp = Mathf.Clamp(hp, 0, defaultHp);
    }
    override protected void HPEqual0() 
    {
        return;
    }
    override public void Attack()
    {
        // Vector2 attackPoint = new Vector2(transform.position.x, transform.position.y);
        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint, attackRange);
        // foreach(Collider2D enemy in hitEnemies){
        //     Debug.Log("Hit");
        // }

        if (saveInput>0){
            Debug.Log("Right");
        }
        if (saveInput<0){
            Debug.Log("Left");
        }
    }
    #endregion
    
    #region Animation
    void MoveAnimationUpdate(Vector2 _moveDir)
    {
        if(_moveDir == Vector2.zero)
        {
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("IsRunning", true);
        }
    }
    #endregion

    #region Player Status Controller
    void Hunger()
    {
        if(isHunger != true)
        {
            return;
        }
        else
        {
            hungerDmgCooldown -= Time.deltaTime;
            if(isHunger && hungerDmgCooldown <= 0)
            {
                TakeDmg(1);
                hungerDmgCooldown = hungerDmgTimer;
            }
        }
    }

    void IsHunger()
    {
        hungerCooldown -= Time.deltaTime;
        if(hungerCooldown <= 0)
        {
            hunger = -1;
            hungerCooldown = hungerTimer;
        }
    }
    #endregion
}

