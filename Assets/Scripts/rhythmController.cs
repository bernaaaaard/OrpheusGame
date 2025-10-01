using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class rhythmController : MonoBehaviour
{
    public Slider slider;
    public AudioClip song;
    public float bpm;
    public float sliderDir = 1;
    private float bps;
    public int count = 0;
    public float time = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = song;
        audioSource.Play();
        slider.value = 0;
        bps = bpm / 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value == 1 || slider.value == 0)
        {
            sliderDir *= -1;
        }
        slider.value += sliderDir * (bps * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.A)) 
        {
            if ((0 - slider.value) > -0.1) 
            {
                count++;
            }
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            if ((1 - slider.value) < 0.1)
            {
                count++;
            }
        }
        Debug.Log(count);
    }
}
