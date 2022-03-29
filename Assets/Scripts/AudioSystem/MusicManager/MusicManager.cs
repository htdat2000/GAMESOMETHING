using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class MusicManager : MonoBehaviour
{
    public Sound[] sounds;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void Start() 
    {
        if(sounds.Length != 0)
        {
            Play(sounds[0].name);
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("Sound: " + name + "Not Found");
            return;
        }
        audioSource.clip = s.audioClip;
        audioSource.volume = s.volume;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("Sound: " + name + "Not Found");
            return;
        }
        audioSource.Stop();
    }
}
