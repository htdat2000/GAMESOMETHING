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

    public bool hadRolled = false;
    public float rollCD  = 5;
    public float rollCDCount;
    public float rollLength = 25f;

    private float attackCooldown = 0.5f;
    private float lastAttack = 0f;
    private List<IInteractables> interactGOs = new List<IInteractables>();

    enum State
    {
        Normal,
        Stun,
        Action
    }

    private State playerState;

    void Start()
    {
        LoadComponent();
        LoadParameter();
    }
    void Update()
    {
        if(playerState == State.Normal)
            Move();
        if(Input.GetKeyDown(KeyCode.T))
            Interact();
        if(Input.GetKeyDown(KeyCode.Space) && CanAttack())
            Attack();
        if(Input.GetKeyDown(KeyCode.I))
            OpenInventory();
        if(Input.GetKeyDown(KeyCode.C))
            OpenCraftingBoard();
        if(rollCDCount >0){
            rollCDCount -= Time.deltaTime;
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
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(rollCDCount > .1f){
                Debug.Log("Cooldown");
            }
            if(rollCDCount <= .1f){
                Debug.Log("Roll"+player.moveDir);
                player.moveDir = moveInput * rollLength;
                rollCDCount = rollCD;
            }
        }
    }
    public void Attack()
    {
        lastAttack = Time.time;
        player.Attack();
    }
    public void Interact()
    {
        if(interactGOs.Count > 0)
        interactGOs[0].Interact();
    }
    // public void Roll(Vector2 moveAmount)
    // {
    //     Vector2 rollDir = moveAmount * 100;
    //     rb.AddForce(rollDir);
    //     // Debug.Log(rollDir.x);
    //     // Debug.Log(rollDir.y);
    // }
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
        }
    }
    void LoadParameter()
    {
        lastAttack = Time.time;
        playerState = State.Normal;
    }
    #endregion

    #region Combat System
    bool CanAttack()
    {
        return lastAttack + attackCooldown < Time.time;
    }
    #endregion
}
