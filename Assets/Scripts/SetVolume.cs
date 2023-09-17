using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    bool isOn;
    float volume;
    public void SetLevel(float slider)
    {
        mixer.SetFloat("Volume", Mathf.Log10(slider) * 20);
    }
}
