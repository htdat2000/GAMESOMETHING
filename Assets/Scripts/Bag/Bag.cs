using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public static Bag bag;
    private List<Items> items = new List<Items>();

    public void AddItem(Items item)
    {
        items.Add(item);
    }

    public void RemoveItem(Items item)
    {
        items.Remove(item);
    }
}
