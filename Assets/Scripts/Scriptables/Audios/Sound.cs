using UnityEngine;
using UnityEngine.Audio;

public class Sound : ScriptableObject 
{
    public string name;
    public AudioClip audioClip;
    [Range(0, 1)] public float volume;
    [HideInInspector] public AudioSource audioSource;
}
