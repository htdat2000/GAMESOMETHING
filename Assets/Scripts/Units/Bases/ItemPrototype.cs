using UnityEngine;

public class ItemPrototype : MonoBehaviour, IInteractables
{
    public Items item;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D colliderComponent;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliderComponent = GetComponent<CapsuleCollider2D>();
    }
    void Start()
    {
        UpdateItemPrototypeData();
    }

    public void UpdateItemPrototypeData()
    {
        spriteRenderer.sprite = item.icon;
        colliderComponent.size = new Vector2(item.sizeX, item.sizeY); 
    }

    public void Interact(Bag bag)
    {
        PickUp(bag);
    }

    void PickUp(Bag bag)
    {
        bag.AddItem(item);
        Destroy(gameObject);
    }

    public void Interact()
    {
        return;
    }
}
