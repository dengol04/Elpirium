using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider slider;
    public AudioSource audio;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volume", 1);
    }

    void Update()
    {
        audio.volume=slider.value;
        PlayerPrefs.SetFloat("volume", slider.value);
    }
}
