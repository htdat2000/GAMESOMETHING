using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDrop
{
    // [SerializeField] private Items item; 
    [SerializeField] public Items item;
    //  { get {return Item;} }
    [SerializeField] private int dropRate; //percentage 

    public bool SpawnItemByDropRate(int randomValue)
    {
        if(randomValue <= dropRate * 10)
        {
            return true;
        }
        else 
        {
            return false;
        }        
    }
}

