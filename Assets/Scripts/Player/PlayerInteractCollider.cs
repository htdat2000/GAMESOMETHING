using UnityEngine;

public class PlayerInteractCollider : MonoBehaviour
{   
    bool isLoaded = false;
    private PlayerController playerController;
    public Bag bag;

    void Start()
    {
        LoadComponent();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<IInteractables>(out playerController.interactGO);
    }
    void OnTriggerExit2D(Collider2D collsion)
    {
        if(playerController.interactGO != null)
        playerController.interactGO = null;
    }

    void Reset() 
    {
        LoadComponent();
    }

    void LoadComponent()
    {
        if(!isLoaded)
        {
            playerController = GetComponentInParent<PlayerController>();
            bag = GetComponentInParent<Bag>();
            isLoaded = !isLoaded;
        }
    }
}
