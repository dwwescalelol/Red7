using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    public static AudioManager instance;
    
    
    private void Start()
    {
        Play("Theme");
    }


    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, s => s.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + s.name + " was not found");
            return;
        }
        s.source.Play();
    }
}
