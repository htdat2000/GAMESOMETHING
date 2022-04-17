using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] protected MusicManager musicManager;
    [SerializeField] protected SFXManager sfxManager; 

    public void MusicSwitch()
    {
        musicManager.gameObject.SetActive(!musicManager.gameObject.activeSelf);
    }

    public void SFXSwitch()
    {
        sfxManager.gameObject.SetActive(!sfxManager.gameObject.activeSelf);
    }
}
