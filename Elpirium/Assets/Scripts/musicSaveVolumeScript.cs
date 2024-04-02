using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicSaveVolumeScript : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("volume", 1);
    }

}
