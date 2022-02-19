using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{   
    public GameEvent onItemChange;
    public int space = 20;
    public List<Items> items = new List<Items>();
    public List<int> amount = new List<int>();
    public Dictionary<Items, int> totalAmount = new Dictionary<Items, int>();
    private Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    public bool AddItem(Items item)
    {   
        if(items.Count >= space)
        {
            if(item.stackAble)
            {
                if(items.Contains(item) && amount[items.LastIndexOf(item)] < 999)
                {
                    amount[items.LastIndexOf(item)]++;
                    AddTotalAmount(item);
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
                    AddTotalAmount(item);
                }
                else
                {
                    items.Add(item);
                    amount.Add(1);
                    AddTotalAmount(item);
                }   
            }
            else
            {
                items.Add(item);
                amount.Add(1);
                AddTotalAmount(item);
            }
            onItemChange.Invoke();
            return true;
        }
    }

    public void RemoveItem(int itemIndex)
    {
        items.RemoveAt(itemIndex);
        amount.RemoveAt(itemIndex);
        onItemChange.Invoke();
    }
    
    public void UseItemInList(int itemIndex)
    {
        items[itemIndex].Use(player);
        RemoveAfterUse(itemIndex);
    }

    private void RemoveAfterUse(int itemIndex)
    {
        if(items[itemIndex].stackAble)
        {
            
            if(IsAmountOfItemEqual1(itemIndex))
            {
                RemoveItem(itemIndex);
            }
            else
            {
                ReduceItemAmount(itemIndex);
            }
        }
        else
        {
            RemoveItem(itemIndex);
        }
    }

    private bool IsAmountOfItemEqual1(int itemIndex) //check whether item's amount equal or more than 1
    {
        if(amount[itemIndex] <= 1)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    private void ReduceItemAmount(int itemIndex)
    {
        amount[itemIndex]--;
        onItemChange.Invoke();
    }

    private void AddTotalAmount(Items item)
    {
        if(totalAmount.ContainsKey(item))
        {
            totalAmount[item]++;
        }
        else
        {
            totalAmount.Add(item, 1);
        }
    }

    public int GetTotalAmount(Items item)
    {
        return totalAmount[item];
    }
}
