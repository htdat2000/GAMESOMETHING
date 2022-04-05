using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] GameObject settingMenu;

    public void SettingMenuSwitch()
    {
        settingMenu.SetActive(!settingMenu.activeSelf);
    }
}
