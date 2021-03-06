using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingBoard : MonoBehaviour
{
    private BlueprintSlot[] blueprintSlot;
    [SerializeField] private GameObject blueprintParent;
    private Blueprints selectedBlueprint = null;
    private Bag bag = null;

    [Header("UI Area")]
    public Sprite nodeSprite;
    public Image[] materialIcons;
    public Image itemCraftIcon;
    public Text[] materialAmountTexts;

    [Header("Item Craft & Materials")]
    private Items itemCraftSelected;
    private Items[] materials = new Items[3];
    private int[] amount = new int[3];

    void Awake()
    {
        blueprintSlot = blueprintParent.GetComponentsInChildren<BlueprintSlot>();
    }

    public void SelectBlueprint(Blueprints _blueprint)
    {
        selectedBlueprint = _blueprint;
        ClearArrayData();
        UpdateUI();
    }

    void UpdateUI()
    {
        itemCraftSelected = selectedBlueprint.itemCraft;
        itemCraftIcon.sprite = selectedBlueprint.itemCraft.icon;
        
        int numberOfMaterials = selectedBlueprint.materials.Length;
        for (int i = 0; i <= numberOfMaterials - 1; i++)
        {
            if(selectedBlueprint.materials[i] != null)
            {
                materials[i] = selectedBlueprint.materials[i];
                materialIcons[i].sprite = materials[i].icon;
                amount[i] = selectedBlueprint.amount[i];
                materialAmountTexts[i].text = amount[i].ToString();
            }
        }
    }

    void ClearArrayData()
    {
        int numberOfMaterials = materials.Length;
        for (int i = 0; i <= numberOfMaterials - 1; i++)
        {
            materials[i] = null;
            materialIcons[i].sprite = nodeSprite;
            amount[i] = 0;
            materialAmountTexts[i].text = "0";
        }
    }

    public void Craft()
    {
        if(CheckBeforeCraft())
        {
            bag.AddItem(itemCraftSelected);
            for (int i = 0; i < selectedBlueprint.materials.Length; i++)
            {
               ReduceMaterial(i); 
            }
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
        if(materials[materialIndex] != null) 
        {   
            if(bag.totalAmount.ContainsKey(materials[materialIndex]))
            {
                int totalAmount = (int)bag.totalAmount[materials[materialIndex]];
                if(totalAmount >= amount[materialIndex])
                {
                    return true;
                }
            }            
            return false;
        }
        else
        {
            if(selectedBlueprint != null)
            {
                return true;
            }
            return false;
        }
    }

    void ReduceMaterial(int materialIndex)
    {
        int totalAmount = (int)bag.totalAmount[materials[materialIndex]];
        int bagLastSlotAmount = bag.ReduceAmountAtLastIndexOfItem(materials[materialIndex], amount[materialIndex]);
        int remainderValue = amount[materialIndex] - bagLastSlotAmount;
        if(bagLastSlotAmount != 0)
        {
            bag.ReduceAmountAtLastIndexOfItem(materials[materialIndex], bagLastSlotAmount);
            bag.ReduceAmountAtLastIndexOfItem(materials[materialIndex], remainderValue);
        }
    }
    public void SetBagComponent(Bag _bag)
    {
        if(bag != null)
        {
            bag = null;
        }
        else 
        {
            bag = _bag;
        }
    }
}
