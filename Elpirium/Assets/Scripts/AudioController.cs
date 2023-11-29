using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider slider;
    public AudioSource audio;

    void Update()
    {
        audio.volume=slider.value;
    }
}
