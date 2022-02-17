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
    public Image firstMaterialIcon;
    public Image secondMaterialIcon;
    public Image thirdMaterialIcon;

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
        firstMaterialIcon.sprite = selectedBlueprint.material[1].icon;
        secondMaterialIcon.sprite = selectedBlueprint.material[2].icon;
        thirdMaterialIcon.sprite = selectedBlueprint.material[3].icon;
    }

    public void Craft()
    {
        
    }

    void CheckBeforeCraft()
    {

    }
}
