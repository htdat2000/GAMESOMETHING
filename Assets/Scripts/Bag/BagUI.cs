﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUI : MonoBehaviour
{
    [SerializeField] private Bag bag;
    private BagSlot[] bagSlots;

    void Awake()
    {
        bagSlots = GetComponentsInChildren<BagSlot>();
    }
    void Start()
    {
        
    }

    public void UpdateUI()
    {
        for (int i = 0; i < bagSlots.Length; i++)
        {   
            if(i < bag.items.Count)
            {
                bagSlots[i].AddItem(bag.items[i]);
            }
            else
            {
                bagSlots[i].ClearSlot();
            }   
        }
    }
}
