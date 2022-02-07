using UnityEngine;

public class PlayerInteractCollider : MonoBehaviour
{   
    [Header("Unity Varibles")]
    public Bag bag;

    [Header("Unity Components")]
    bool isLoaded = false;
    private PlayerController playerController;
    

    void Start()
    {
        LoadComponent();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractables interactGO;
        collision.TryGetComponent<IInteractables>(out interactGO);
        if(interactGO != null)
        {
            playerController.AddInteractableGO(interactGO);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        IInteractables interactGO;
        collision.TryGetComponent<IInteractables>(out interactGO);
        if(interactGO != null)
        {
            playerController.RemoveInteractableGO(interactGO);
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
            playerController = GetComponentInParent<PlayerController>();
            bag = GetComponentInParent<Bag>();
            isLoaded = !isLoaded;
        }
    }
}
