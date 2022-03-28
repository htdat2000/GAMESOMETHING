using UnityEngine;

public class ItemPrototype : MonoBehaviour, IInteractables
{   
    public Items item;
    private Bag bag;

    [Header("Unity Components")]
    private bool isLoaded = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CapsuleCollider2D colliderComponent;
    
    void Awake()
    {
        LoadComponent();
    }
    void Start()
    {
        UpdateItemPrototypeData();
    }

    public void UpdateItemPrototypeData()
    {
        if(item == null)
        {
            Debug.LogError("No Item Data");
            Destroy(gameObject);
            return;
        }
        spriteRenderer.sprite = item.icon;
        colliderComponent.size = new Vector2(item.sizeX, item.sizeY); 
    }
    public void Interact()
    {
        PickUp(bag);
    }

    void PickUp(Bag _bag)
    {   if(_bag != null)
        {
        bool wasPickedUp = _bag.AddItem(item);
        if(wasPickedUp)
            {
            Destroy(gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
       PlayerInteractCollider player; 
       collision.TryGetComponent<PlayerInteractCollider>(out player);
       if(player != null)
       {
           bag = player.bag;
       }
    }
    void OnTriggerExit2D(Collider2D collsion)
    {
        if(bag != null)
        bag = null;
    }

    void Reset() 
    {
        LoadComponent();
    }

    void LoadComponent()
    {
        if(!isLoaded)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            colliderComponent = GetComponent<CapsuleCollider2D>();
            isLoaded = !isLoaded;
        }
    }
}
