using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : Creatures
{
    public float attackRange = 0f;
    private float _hunger;
    private float _stamina;
 
    [Header("Default Value")]
    private float defaultHunger = 100;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private int defaultHp;
    [SerializeField] private float defaultStamina = 100;
    private float staminaRefillCooldown = 3f;
    private float lastRefillStamina = 0f;

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
    
    [Header("Effect")]
    [SerializeField] private GameObject attackEffect;

    [Header("UI pointer")]
    [SerializeField] private Slider HPBar;
    [SerializeField] private Slider SPBar;
    [SerializeField] private Slider CPBar;
    
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
    public float stamina
    { 
        get 
        {
            return _stamina;
        } 
        set 
        {
            _stamina += value;
            _stamina = Mathf.Clamp(_stamina, 0, 100);
        } 
    }
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bag = GetComponent<Bag>();
        anim = GetComponent<Animator>();
        LoadParameter();
    }
    void Start()
    {
        UISetup();
    }

    void Update()
    {
        Hunger();
        IsHunger();
        StaminaRefill();
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
    // override public void Attack()
    // {
    //     // Vector2 attackPoint = new Vector2(transform.position.x, transform.position.y);
    //     // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint, attackRange);
    //     // foreach(Collider2D enemy in hitEnemies){
    //     //     Debug.Log("Hit");
    //     // }

    //     if (saveInput>0){
    //         Debug.Log("Right");
    //     }
    //     if (saveInput<0){
    //         Debug.Log("Left");
    //     }
    // }
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
            CPBar.value = hunger;
        }
    }

    public void StaminaDecrease(float value)
    {
        stamina = -value;
        SPBar.value = stamina;
    }
    public void StaminaIncrease(float value)
    {
        stamina = +value;
        SPBar.value = stamina;
    }
    void StaminaRefill()
    {
        if(lastRefillStamina + staminaRefillCooldown < Time.time)
        {
            lastRefillStamina = Time.time;
            StaminaIncrease(5f);
            SPBar.value = stamina;
        }
    }

    #endregion

    #region Player Action Controller
    override public void Attack()
    {
        if(stamina >= 10f)
        {
            Instantiate(attackEffect, transform.position, Quaternion.identity);
            StaminaDecrease(10f);
        }
    }

    #endregion

    #region UI Setup
    void UISetup()
    {
        SPBar.maxValue = defaultStamina;
    }
    #endregion

    #region Setup
    void LoadParameter()
    {
        lastRefillStamina = Time.time;
        stamina = defaultStamina;
        hp = defaultHp;
        hunger = defaultHunger;
        speed = defaultSpeed;
    }
    #endregion
}

