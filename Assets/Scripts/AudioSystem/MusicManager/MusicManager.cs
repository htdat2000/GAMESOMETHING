using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class MusicManager : MonoBehaviour
{
    public Music[] musics;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }
    
    void Start() 
    {
        if(musics.Length != 0)
        {
            Play(musics[0].name);
        }
    }

    public void Play(string name)
    {
        Music m = Array.Find(musics, music => music.name == name);
        if(m == null)
        {
            Debug.Log("Music: " + name + "Not Found");
            return;
        }
        audioSource.clip = m.audioClip;
        audioSource.volume = m.volume;
        audioSource.Play();
    }

    public void Stop(string name)
    {
        Music m = Array.Find(musics, music => music.name == name);
        if(m == null)
        {
            Debug.Log("Music: " + name + "Not Found");
            return;
        }
        audioSource.Stop();
    }
}
