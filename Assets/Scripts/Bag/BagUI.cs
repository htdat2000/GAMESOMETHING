using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagUI : MonoBehaviour
{
    [SerializeField] Bag bag;
    private List<BagSlot> bagSlots = new List<BagSlot>();

    void Awake()
    {
        GetComponentsInChildren<BagSlot>(bagSlots);
    }
    void Start()
    {
        for (int i = 0; i < bagSlots.Count; i++)
        {
            bagSlots[i].item = bag.items[i];
        }
    }
}
