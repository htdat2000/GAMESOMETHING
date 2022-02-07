using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagSlot : MonoBehaviour
{
    [Header("Item Area")]
    public Items item;
    public Image icon;

    [Header("Unity Script Setup")]
    private int tapCount = 0;

    [Header("Unity Components")]
    [SerializeField] private Player player;
    [SerializeField] private Bag bag;

    public void AddItem(Items _item)
    {
        item = _item;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void Use()
    {
        if(IsDoubleTap())
        {
            if(item != null)
            {
                item.Use(player);
                bag.RemoveAfterUse(item);
            }
        }   
    }
    bool IsDoubleTap()
    {
        IsPress();
        if(tapCount >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void IsPress()
    {
        if(tapCount == 0)
        {
            tapCount++;
            Invoke("ResetTapCount", 0.2f);
        }
        else //if(tapCount < 2)
        {
            tapCount++; 
        }
    }

    void ResetTapCount()
    {
        tapCount = 0;
    }
}
