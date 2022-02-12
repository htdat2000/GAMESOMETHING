using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{   
    public GameEvent onItemChange;
    public int space = 20;
    public List<Items> items = new List<Items>();
    public List<int> amount = new List<int>();

    public bool AddItem(Items item)
    {   
        if(items.Count >= space)
        {
            if(item.stackAble)
            {
                if(items.Contains(item) && amount[items.LastIndexOf(item)] < 999)
                {
                    amount[items.LastIndexOf(item)]++;
                    onItemChange.Invoke();
                    return true;
                }
            }     
            onItemChange.Invoke();  
            return false;     
        }
        else 
        {
            if(item.stackAble)
            {
                if(items.Contains(item) && amount[items.LastIndexOf(item)] < 999)
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
            onItemChange.Invoke();
            return true;
        }
    }

    public void RemoveItem(Items item)
    {
        int index = items.LastIndexOf(item);
        items.RemoveAt(index);
        amount.RemoveAt(index);
        onItemChange.Invoke();
    }
    
    public void RemoveAfterUse(Items item)
    {
        if(item.stackAble)
        {
            
            if(IsAmountOfItemEqual1(item))
            {
                RemoveItem(item);
            }
            else
            {
                ReduceItemAmount(item);
            }
        }
        else
        {
            RemoveItem(item);
        }
    }

    private bool IsAmountOfItemEqual1(Items item) //check whether item's amount equal or more than 1
    {
        int index = items.LastIndexOf(item);
        if(amount[index] <= 1)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    private void ReduceItemAmount(Items item)
    {
        int index = items.LastIndexOf(item);
        amount[index]--;

        onItemChange.Invoke();
    }
}
