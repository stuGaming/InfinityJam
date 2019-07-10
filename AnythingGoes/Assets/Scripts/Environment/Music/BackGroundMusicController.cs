using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class BackGroundMusicController : MonoBehaviour
{
    [SerializeField]
    AudioSource source;
    private AudioClip nextClip;
    public List<AudioClip> clips = new List<AudioClip>();
    private void Start()
    {
        nextClip = clips[0];


        Mediator.RegisterHandler(GameEvents.AudioSwitch, this, audioSwitch);
    }
    private void Update()
    {
        
        if (!source.isPlaying)
        {
            source.clip = nextClip;
            source.Play();
        }
    }
    private void audioSwitch(Message message)
    {
        int x =(int) message.Properties[GameEventProperties.Audio];
        nextClip = clips[x];
    }
}
