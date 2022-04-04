﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
// [RequireComponent(typeof(Animator))]
public class Player : Creatures
{
    public float attackRange = 0f;
    private float _hunger;
    private float _stamina;
    private int _hp;
 
    [Header("Default Value")]
    private float defaultHunger = 100;
    [SerializeField] private int defaultDmg;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private int defaultHp = 100;
    [SerializeField] private float defaultStamina = 100;
    private float staminaRefillCooldown = 0.5f;
    private float lastRefillStamina = 0f;
    [SerializeField] private GameEvent onScreenShake;

    [Header("PlayerState")]
    private State playerState = State.Normal;
    enum State
    {
        Normal,
        Attacked,
        Stun,
        Action
    }
    

    [Header("Unity Components")]
    private Bag bag;
    private PlayerController playerController;
    private Rigidbody2D rb;
    private AttackEffect attackEffectScript;

    [Header("Unity Script Variables")]
    [HideInInspector]public Vector2 moveDir;
    
    [Header("Hunger Function Variables")]
    private float hungerCooldown = 0;
    private float hungerTimer = 2;
    private bool isHunger = false;
    private float hungerDmgCooldown = 0;
    private float hungerDmgTimer = 1;
    
    [Header("Effect")]
    [SerializeField] private GameObject attackEffect;
    [SerializeField] private GameObject dustEffect;

    [Header("UI pointer")]
    [SerializeField] private Slider HPBar;
    [SerializeField] private Slider SPBar;
    [SerializeField] private Slider CPBar;

    [Header("Const")]
    protected const float KNOCKBACK_TIME = 0.2f;
    protected const float ATTACKED_TIME = 0.2f;
    protected const float ROLL_FORCE = 5;
    protected const float ROLL_TIME = 0.2f;
    
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
            _hunger = Mathf.Clamp(_hunger, 0, defaultHunger);
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
            _stamina = Mathf.Clamp(_stamina, 0, defaultStamina);
            SPBar.value = stamina;
        } 
    }

    public int Hp
    {
        get 
        {
            return hp;
        } 
        set 
        {
            hp += value;
            hp = Mathf.Clamp(hp, 0, defaultHp);
            HPBar.value = hp;
        } 
    }
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bag = GetComponent<Bag>();
        playerController = GetComponent<PlayerController>();
        attackEffectScript = attackEffect.GetComponent<AttackEffect>();
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
        if(playerState == State.Normal)
        {  
            rb.velocity = moveDir * speed;
            playerController.MoveAnimationUpdate(moveDir);
        }
    }
    override protected void Die()
    {
        PlaySFX(SFX.SFXState.DieSFX);
        CheckPointManager.instance.RespawnPlayer(this.gameObject);
        Debug.Log("The Player has died");
    }
    override public void TakeDmg(int dmg)
    {
        if(playerState == State.Normal)
        {
            PlaySFX(SFX.SFXState.HurtSFX);
            playerController.InvisibleAnimPlay();
            playerState = State.Attacked;
            StartCoroutine(AttackedOff());
            Hp = -dmg;
            onScreenShake.Invoke();
            HPEqual0();
        }      
    }
    override protected void HPEqual0() 
    {
        if(Hp <= 0)
        {
            Die();
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
                Hp = -1;
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
        // SPBar.value = stamina;
    }
    public void StaminaIncrease(float value)
    {
        stamina = +value;
        // SPBar.value = stamina;
    }
    void StaminaRefill()
    {
        if(lastRefillStamina + staminaRefillCooldown < Time.time)
        {
            lastRefillStamina = Time.time;
            StaminaIncrease(3f);
        }
    }

    #endregion

    #region Player Action Controller
    override public void Attack()
    {
        if(stamina >= 10f && playerState == State.Normal)
        {
            PlaySFX(SFX.SFXState.AttackSFX);
            Instantiate(attackEffect, transform.position, Quaternion.identity);
            StaminaDecrease(10f);
        }
    }

    #endregion

    #region UI Setup
    void UISetup()
    {
        SPBar.maxValue = defaultStamina;
        HPBar.maxValue = defaultHp;
    }
    #endregion

    #region Setup
    void LoadParameter()
    {
        dmg = defaultDmg;
        lastRefillStamina = Time.time;
        stamina = defaultStamina;
        hp = defaultHp;
        hunger = defaultHunger;
        speed = defaultSpeed;

        attackEffectScript.dmg = dmg;
    }
    #endregion

    #region Player Effect
    public void KnockbackEffect(GameObject attacker)
    {
        if(playerState == State.Attacked)
        {
            StartCoroutine(KnockBackOff());
            Vector3 direction = this.gameObject.transform.position - attacker.transform.position;
            rb.velocity = direction.normalized * 2;
        }
    }
    public bool Roll()
    {
        bool canRoll = stamina >= 10f && playerState == State.Normal;
        if(canRoll)
        {
            Instantiate(dustEffect, transform.position, Quaternion.identity);
            ChangeStatus("Action");
            StartCoroutine(RollEnd());
            Vector3 direction = new Vector3(moveDir.x,moveDir.y,0f);
            rb.velocity = direction.normalized * ROLL_FORCE;
            StaminaDecrease(10f);
        }
        return canRoll;
    }
    #endregion
    #region Status Field
    protected IEnumerator KnockBackOff()
    {
        yield return new WaitForSeconds(KNOCKBACK_TIME);
        rb.velocity = Vector2.zero;
    }

    protected IEnumerator AttackedOff()
    {
        yield return new WaitForSeconds(ATTACKED_TIME);
        playerState = State.Normal;
    }

    protected IEnumerator RollEnd()
    {
        yield return new WaitForSeconds(ROLL_TIME);
        rb.velocity = Vector2.zero;
        playerState = State.Normal;
    }

    public void ChangeStatus(string status)
    {
        switch (status)
        {
            case "Action":
                playerState = State.Action;
                break;
            case "Attacked":
                playerState = State.Attacked;
                break;
            case "Normal":
                playerState = State.Normal;
                break;
            case "Stun":
                playerState = State.Stun;
                break;
            default:
                playerState = State.Normal;
                break;
        }
    }
    #endregion
}

