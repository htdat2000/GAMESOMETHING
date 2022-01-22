using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private bool isLoaded = false;
    private Player player;
    private Rigidbody2D rb;
    public float attackRange = 0f;
    bool openStatus = false;
    public IInteractables interactGO;

    void Start()
    {
        LoadComponent();
    }
    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.T) && interactGO != null)
        {
            Interact();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory();
        }
    }

    public void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 moveAmount = moveInput.normalized;
        player.moveDir = moveAmount;
        
        /*if(Input.GetKeyDown(KeyCode.Q))
        {
            Roll(moveAmount);
        }*/
    }

    public void Attack()
    {
        player.Attack();
    }
    public void Interact()
    {
        interactGO.Interact();
    }
    /*public void Roll(Vector2 moveAmount)
    {
        Vector2 rollDir = moveAmount * 1.5f;
        rb.AddForce(rollDir);
        Debug.Log(rollDir.x);
        Debug.Log(rollDir.y);
    }*/
    public void OpenInventory()
    {
        if(openStatus==false)
            Debug.Log("Inventory Opened");
        else
            Debug.Log("Inventory Closed");
        openStatus = !openStatus;
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
        }
    }
}
