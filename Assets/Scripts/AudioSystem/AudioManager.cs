using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] Image musicImg;
    [SerializeField] Sprite musicIcon;
    [SerializeField] Sprite muteMusicIcon;
    [SerializeField] protected MusicManager musicManager;

    [Header("SFX")]
    [SerializeField] Image sfxImg;
    [SerializeField] Sprite sfxIcon;
    [SerializeField] Sprite muteSFXIcon;
    [SerializeField] protected SFXManager sfxManager;
     
    public void MusicSwitch()
    {
        if(musicManager.gameObject.activeSelf)
        {
            musicImg.sprite = muteMusicIcon;
        }
        else
        {
            musicImg.sprite = musicIcon;
        }
        musicManager.gameObject.SetActive(!musicManager.gameObject.activeSelf);
    }

    public void SFXSwitch()
    {
        if(sfxManager.gameObject.activeSelf)
        {
            sfxImg.sprite = muteSFXIcon;
        }
        else
        {
            sfxImg.sprite = sfxIcon;
        }
        sfxManager.gameObject.SetActive(!sfxManager.gameObject.activeSelf);
    }
}
