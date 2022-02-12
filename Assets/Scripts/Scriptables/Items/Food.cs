using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Items/Food")]
public class Food : Consumable
{
    public int healthValue;
    public int hungerValue;

    public override void Use(Player player)
    {
        player.hunger = hungerValue;
    }
}
