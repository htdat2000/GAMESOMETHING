using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BagSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Item Area")]
    public Items item;
    public Image icon;

    [Header("Unity Script Setup")]
    private int tapCount = 0;
    public int slotIndex;

    [Header("Unity Components")]
    [SerializeField] private Bag bag;

    #region Basic Function (Add, Clear, Use Item)
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
            if(item != null && item.usable == true)
            {
                bag.UseItemInList(slotIndex);
            }
            else
            {
                Debug.Log("Can't use this item");
            }
        }   
    }
    #endregion

    #region Check Interact Function
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
    #endregion

    #region Tooltip
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.tooltipSystem.Show("Items_Description", item.name_);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.tooltipSystem.Hide();
    }
    #endregion
}
