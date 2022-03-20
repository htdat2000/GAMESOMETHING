using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public GameObject bagUI;
    public GameObject craftUI;

    [Header("Unity Components")]
    private bool isLoaded = false;
    private Player player;
    private Rigidbody2D rb;
    private CraftingBoard craftingBoard;
    private Bag bag;

    [Header("Unity Script Variables")]
    public float attackRange = 0f;
    public float moveSpeed;

    private float attackCooldown = 0.5f;
    private float lastAttack = 0f;

    private float lastRoll = 0f;
    [SerializeField] float rollCooldown = 1f;

    private Animator anim;
    private List<IInteractables> interactGOs = new List<IInteractables>();

    void Start()
    {
        LoadComponent();
        LoadParameter();
    }
    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.T))
            Interact();
        if(Input.GetKeyDown(KeyCode.Space) && CanAttack())
            Attack();
        if(Input.GetKeyDown(KeyCode.I))
            OpenInventory();
        if(Input.GetKeyDown(KeyCode.C))
            OpenCraftingBoard();
        if(Input.GetKeyDown(KeyCode.Z) && CanRoll())
            Roll();
        if(Input.GetKeyDown(KeyCode.L))
        {
            anim.Play("Dance");
        }
    }

    #region Player controller method
    public void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(Input.GetKey(KeyCode.LeftShift) && player.stamina > 1f && moveInput != Vector2.zero){
            Vector2 moveAmount = moveInput.normalized * 1.2f;
            player.StaminaDecrease(0.5f);
            player.moveDir = moveAmount;
        }
        else{
            Vector2 moveAmount = moveInput.normalized;
            player.moveDir = moveAmount;
        }
    }
    public void Attack()
    {
        lastAttack = Time.time;
        player.Attack();
    }
    private void Roll()
    {
        if(player.Roll())
        {
            lastRoll = Time.time;
            anim.Play("Roll");
        }
    }
    public void Interact()
    {
        if(interactGOs.Count > 0)
        {
            interactGOs[0].Interact();
        }        
    }
    public void OpenInventory()
    {
        bagUI.SetActive(!bagUI.activeSelf);
    }

    public void OpenCraftingBoard()
    {
        craftUI.SetActive(!craftUI.activeSelf);
        if(craftingBoard == null)
        {
            craftingBoard = craftUI.GetComponentInParent<CraftingBoard>();
        }
        if(bag == null)
        {
            bag = GetComponent<Bag>();
        }
        craftingBoard.SetBagComponent(bag);
    }
    #endregion

    #region Setup method
    public void AddInteractableGO(IInteractables interactGO)
    {
        if(!interactGOs.Contains(interactGO))
        {
            interactGOs.Add(interactGO);
        }
    }
    public void RemoveInteractableGO(IInteractables interactGO)
    {
        if(interactGOs.Contains(interactGO))
        {
            interactGOs.Remove(interactGO);
        }
    }
    void Reset() 
    {
        LoadComponent();
    }
    void LoadComponent()
    {
        if(!isLoaded)
        {
            player = GetComponent<Player>();
            rb = GetComponent<Rigidbody2D>();
            isLoaded = !isLoaded;
            anim = GetComponent<Animator>();
        }
    }
    void LoadParameter()
    {
        lastAttack = Time.time;
        lastRoll = Time.time;
    }
    #endregion

    #region Combat System
    bool CanAttack()
    {
        return lastAttack + attackCooldown < Time.time;
    }
    bool CanRoll()
    {
        return lastRoll + rollCooldown < Time.time;
    }
    #endregion

    #region Animation
    public void MoveAnimationUpdate(Vector2 _moveDir)
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
}
