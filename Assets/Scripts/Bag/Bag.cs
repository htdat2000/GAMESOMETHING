using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{   
    private int space = 20;
    private List<Items> items = new List<Items>();
    private List<int> amount = new List<int>();

    public bool AddItem(Items item)
    {   
        if(items.Count >= space)
        {
            return false;
        }
        else 
        {
            if(item.stackAble)
            {
                if(items.Contains(item) && amount[items.LastIndexOf(item)] < 999)
                {
                    amount[items.LastIndexOf(item)]++;
                    return true;
                }
                items.Add(item);
                amount.Add(1);
            }
            else
            {
                items.Add(item);
                amount.Add(1);
            }
            return true;
        }  
    }

    public void RemoveItem(Items item)
    {
        items.Remove(item);
    }
}
