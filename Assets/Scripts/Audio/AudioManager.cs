﻿using OVRSimpleJSON;
using System;
using System.IO;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public SoundInfo[] soundInfos;
    private JSONNode info;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Get audioclips from JSON
        TextAsset textfromfile = Resources.Load<TextAsset>("Audio");
        using (StreamReader sr = new StreamReader(new MemoryStream(textfromfile.bytes)))
        {
            string json = sr.ReadToEnd();
            info = JSON.Parse(json);
            soundInfos = JsonHelper.getJsonArray<SoundInfo>(info["files"].ToString());
            sr.Close();
        }


        sounds = new Sound[soundInfos.Length];
        for (int i = 0; i < soundInfos.Length; i++)
        {
            Sound sound = new Sound(soundInfos[i]);
            sounds[i] = sound;
        }


        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;    

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Debug.Log("Play called");

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log($"Sound: {name} not found!");
            return;
        }
        s.source.Play();
    }
}