using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
 
public class AudioSettingsButtonBehaviour : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMusicVolume (float musicVolume)
    {
        audioMixer.SetFloat("musicVolume", musicVolume);
    }

    public void SetEffectsVolume(float effectsVolume)
    {
        audioMixer.SetFloat("effectsVolume", effectsVolume);
    }

}
