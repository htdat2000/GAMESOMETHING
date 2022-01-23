using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagSlot : MonoBehaviour
{
    public Items item;

    public void AddItem(Items _item)
    {
        item = _item;
    }

    public void ClearSlot()
    {

    }
}
