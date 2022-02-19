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
    private Items[] materials;
    private int[] amount;

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
        if(bag.totalAmount[selectedBlueprint.materials[materialIndex]] >= selectedBlueprint.amount[materialIndex])
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
        if(bag.totalAmount[selectedBlueprint.materials[materialIndex]] >= selectedBlueprint.amount[materialIndex])
        {

        }
    }
}
