using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingBoard : MonoBehaviour
{
    private BlueprintSlot[] blueprintSlot;
    [SerializeField] private GameObject blueprintParent;
    [HideInInspector] public Blueprints selectedBlueprint;
    private Bag bag;

    [Header("UI Area")]
    public Image selectedBlueprintIcon;
    public Image[] materialIcons;

    [Header("Item Craft & Materials")]
    private Items itemCraftSelected;
    private Items[] materials = new Items[3];
    private int[] amount = new int[3];

    void Start()
    {
        blueprintSlot = blueprintParent.GetComponentsInChildren<BlueprintSlot>();
    }

    public void SelectBlueprint(Blueprints _blueprint)
    {
        selectedBlueprint = _blueprint;
        UpdateUI();
    }

    void UpdateUI()
    {
        selectedBlueprintIcon.sprite = selectedBlueprint.itemCraft.icon;
        for (int i = 0; i < 2; i++)
        {
            materials[i] = selectedBlueprint.materials[i];
            materialIcons[i].sprite = materials[i].icon;
            amount[i] = selectedBlueprint.amount[i];
        }
    }

    public void Craft()
    {
        if(CheckBeforeCraft())
        {
            for (int i = 0; i < 2; i++)
            {
               ReduceMaterial(i); 
            }
            bag.AddItem(itemCraftSelected);
            Debug.Log("An item has been crafted");
        }
    }

    bool CheckBeforeCraft()
    {
        if(CheckMaterialAmount(0) && CheckMaterialAmount(1) && CheckMaterialAmount(2))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    bool CheckMaterialAmount(int materialIndex)
    {
        if(bag.totalAmount[materials[materialIndex]] >= amount[materialIndex])
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    void ReduceMaterial(int materialIndex)
    {
        int totalAmount = bag.totalAmount[materials[materialIndex]];
        int bagLastSlotAmount = bag.ReduceAmountAtLastIndexOfItem(materials[materialIndex], amount[materialIndex]);
        int remainderValue = amount[materialIndex] - bagLastSlotAmount;
        if(bagLastSlotAmount != 0)
        {
            bag.ReduceAmountAtLastIndexOfItem(materials[materialIndex], bagLastSlotAmount);
            bag.ReduceAmountAtLastIndexOfItem(materials[materialIndex], remainderValue);
        }
    }
}
