﻿using UnityEngine;

public class ItemPrototype : MonoBehaviour, IInteractables
{
    public Items item;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D colliderComponent;
    private Bag bag;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
       collision.TryGetComponent<Bag>(out bag);
    }
    void OnTriggerExit2D(Collider2D collsion)
    {
        if(bag != null)
        bag = null;
    }
}
