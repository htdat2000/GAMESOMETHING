using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    private Player player;
    public float attackRange = 0f;
    bool openStatus = false;
    public IInteractables interactGO;
    void Start()
    {
        player = GetComponent<Player>();
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
        player.Move();
    }
    public void Attack()
    {
        player.Attack();
    }
    public void Interact()
    {
        interactGO.Interact();
    }
    public void OpenInventory()
    {
        if(openStatus==false)
            Debug.Log("Inventory Opened");
        else
            Debug.Log("Inventory Closed");
        openStatus = !openStatus;
    }
}
