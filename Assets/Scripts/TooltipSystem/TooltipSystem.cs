using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Components;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem tooltipSystem;
    RectTransform rectTransform;

    [Header("Tooltip UI")]

    [SerializeField] LayoutElement layoutElement;
    [SerializeField] TextMeshProUGUI contentText;
    
    [SerializeField] LocalizeStringEvent content;

    private int characterWrapLimit = 100;

    void Start()
    {
        if(tooltipSystem != null)
        {
            Debug.LogError("More than 1 Tooltip System in scene");
            return;
        }
        tooltipSystem = this;
        rectTransform = GetComponent<RectTransform>();
    }

    public void Show(string table, string key)
    {
        //contentKey.Localization.TableReference = "Item_Description";
        //contentKey.LocalizedString.TableEntryReference = key;
        content.StringReference.TableReference = table;
        content.StringReference.TableEntryReference = key;
        SetTooltipPosition();
        content.gameObject.SetActive(true);
    }

    public void Hide()
    {
        content.gameObject.SetActive(false);
    }

    void LayoutElementSwitch()
    {
        int contentTextLength = contentText.text.Length;
        layoutElement.enabled = (contentTextLength > characterWrapLimit) ? true : false;
    }

    void SetTooltipPosition()
    {
        Vector2 position = Input.mousePosition;
        transform.position = position;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;
        rectTransform.pivot = new Vector2(pivotX, pivotY);
    }





}
