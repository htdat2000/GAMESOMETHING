using UnityEngine;

public class PlayerInteractCollider : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
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
}
