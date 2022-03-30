using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "newSFX", menuName = "Sound/SFX")]
public class SFX : Sound
{
    [System.Serializable]
    public class SFXType
    {
        public SFXState type;
        public AudioClip clip;
    }

    public enum SFXState
    {
        HurtSFX,
        AttackSFX,
        DieSFX
    }
    public SFXType[] types;

    public void PlaySFX(SFXState state)
    {
        AudioClip _audioClip = Array.Find(types, type => type.type == state).clip;
        if(_audioClip != null)
        {
            SFXManager.sfxManager.PlaySFX(_audioClip);
        }    
    }
}
