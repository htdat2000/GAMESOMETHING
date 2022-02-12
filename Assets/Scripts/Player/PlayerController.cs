using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public GameObject bagUI;
    
    [Header("Unity Components")]
    private bool isLoaded = false;
    private Player player;
    private Rigidbody2D rb;
    
    [Header("Unity Script Varibles")]
    public float attackRange = 0f;
    private List<IInteractables> interactGOs = new List<IInteractables>();

    void Start()
    {
        LoadComponent();
    }
    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.T))
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

    #region Player controller method
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
        if(interactGOs.Count > 0)
        interactGOs[0].Interact();
    }
    /*public void Roll(Vector2 moveAmount)
    {
        Vector2 rollDir = moveAmount * 100;
        rb.AddForce(rollDir);
        Debug.Log(rollDir.x);
        Debug.Log(rollDir.y);
    }*/
    public void OpenInventory()
    {
        bagUI.SetActive(!bagUI.activeSelf);
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
    #endregion
}
