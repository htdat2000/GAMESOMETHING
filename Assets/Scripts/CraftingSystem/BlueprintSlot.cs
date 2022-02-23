using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintSlot : MonoBehaviour
{
    public Image blueprintIcon;
    public Blueprints blueprint;
    public CraftingBoard craftingBoard;

    void Start()
    {
        if(blueprintIcon != null && blueprint != null)
        {
            blueprintIcon.sprite = blueprint.itemCraft.icon;
        }
    }

    public void SelectBlueprint()
    {
        if(blueprint != null)
        {
            craftingBoard.SelectBlueprint(blueprint);
        }
    }
}
