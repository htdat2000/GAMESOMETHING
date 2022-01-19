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
            if(items.Contains(item))
            {
                if(item.stackAble)
                {
                    if(amount[items.LastIndexOf(item)] < 999)
                    {
                        amount[items.LastIndexOf(item)]++;
                    }
                    else 
                    {
                        items.Add(item);
                        amount.Add(1);
                    }
                }
                else 
                {
                    items.Add(item);
                    amount.Add(1);
                }
            }
            else
            {
                items.Add(item);
                amount.Add(1);
            }
        }
        return true;
    }

    public void RemoveItem(Items item)
    {
        items.Remove(item);
    }
}
