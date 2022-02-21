using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Blueprint", menuName = "Blueprint")]
public class Blueprints : ScriptableObject
{
   public Items itemCraft;
   public Items[] materials;
   public int[] amount;
}
