using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    public static SFXManager sfxManager;
    AudioSource audioSource;
    [SerializeField] Slider sfxVolume;
    
    void Awake() 
    {
        if(sfxManager != null)
        {
            Debug.LogError("More Than 1 SFXManager In Scene");
            return;
        }
        sfxManager = this;
        audioSource = GetComponent<AudioSource>();
    }
    
    public void PlaySFX(AudioClip clip)
    {
        if(this.gameObject.activeSelf)
        {
            audioSource.PlayOneShot(clip, sfxVolume.value);
        }
    }
}
