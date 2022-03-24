using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Components;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem tooltipSystem;

    [Header("Tooltip UI")]
    [SerializeField] TextMeshProUGUI content;
    [SerializeField] LocalizeStringEvent contentKey;

    void Start()
    {
        if(tooltipSystem != null)
        {
            Debug.LogError("More than 1 Tooltip System in scene");
            return;
        }
        tooltipSystem = this;
    }

    public void Show(string table, string key)
    {
        //contentKey.Localization.TableReference = "Item_Description";
        //contentKey.LocalizedString.TableEntryReference = key;
        contentKey.StringReference.TableReference = table;
        contentKey.StringReference.TableEntryReference = key;
        content.gameObject.SetActive(true);
    }

    public void Hide()
    {
        content.gameObject.SetActive(false);
    }




}
