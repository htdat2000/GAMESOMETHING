using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{   
    public GameEvent onItemChange;
    public int space = 20;
    private int maxNumberOfItem = 999;
    public List<Items> items = new List<Items>();
    public List<int> amount = new List<int>();
    public Hashtable totalAmount = new Hashtable();
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
                if(items.Contains(item) && amount[items.LastIndexOf(item)] < maxNumberOfItem)
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
                if(items.Contains(item) && amount[items.LastIndexOf(item)] < maxNumberOfItem)
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
            AddTotalAmount(item);
            onItemChange.Invoke();
            return true;
        }
    }
    public void RemoveItem(int itemIndex) //remove by order of item in list
    {
        items.RemoveAt(itemIndex);
        amount.RemoveAt(itemIndex);
        onItemChange.Invoke();
    }
    public void RemoveItem(Items item) //remove by type of item
    {
        int index = items.LastIndexOf(item);
        items.RemoveAt(index);
        amount.RemoveAt(index);
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
        ReduceTotalAmount(itemIndex, 1);
        onItemChange.Invoke();
    }
    private void AddTotalAmount(Items item)
    {
        if(totalAmount.ContainsKey(item))
        {
            int amountValue = (int)totalAmount[item] + 1;
            totalAmount[item] = amountValue;
        }
        else
        {
            totalAmount.Add(item, 1);
        }
    }
    private void ReduceTotalAmount(int itemIndex, int amountReduce)
    {
        Items itemReduce = items[itemIndex];
        int amountValue = (int)totalAmount[itemReduce];
        if(amountValue >= amountReduce)
        {
            amountValue -= amountReduce;
            if(amountValue == 0)
            {
                totalAmount.Remove(itemReduce);
            }
            else
            {
               totalAmount[itemReduce] = amountValue; 
            }
        }
    }
    private void ReduceTotalAmount(Items item, int amountReduce)
    {   
        int amountValue = (int)totalAmount[item];
        if(amountValue >= amountReduce)
        {
            amountValue -= amountReduce;
            if(amountValue == 0)
            {
                totalAmount.Remove(item);
            }
            else
            {
                totalAmount[item] = amountValue;
            }
        }
    }
    public int ReduceAmountAtLastIndexOfItem(Items item, int amountReduce)
    {   
        int index = items.LastIndexOf(item);
        if(amount[index] < amountReduce)
        {
            return amount[index];
        }
        amount[index] -= amountReduce;
        ReduceTotalAmount(item, amountReduce);
        if(amount[index] == 0)
        {
            RemoveItem(index);
        }
        onItemChange.Invoke();
        return 0;
    }
}
