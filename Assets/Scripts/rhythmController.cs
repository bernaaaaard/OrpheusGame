using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class rhythmController : MonoBehaviour
{
    public AudioClip song;
    public float bpm;
    public float bps;
    public AudioSource audioSource; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource.clip = song;
        bps = 60 / bpm;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying) 
        {
            audioSource.Play();
        }
    }
}
