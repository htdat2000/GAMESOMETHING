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
        player.Move();
        Interact();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            player.Attack();
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory();
            openStatus = !openStatus;
        }
    }
    public void Interact()
    {
        if(Input.GetKeyDown(KeyCode.T) && interactGO != null)
        {
        interactGO.Interact();
        }
    }
    void OpenInventory(){
        if(openStatus==false)
            Debug.Log("Inventory Opened");
        if(openStatus==true)
            Debug.Log("Inventory Closed");
    }
}
