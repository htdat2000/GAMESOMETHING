﻿using System.Collections;
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
        blueprintIcon.sprite = blueprint.itemCraft.icon;
    }

    public void SelectBlueprint()
    {
        craftingBoard.SelectBlueprint(blueprint);
    }
}
