using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsButtonBehaviour : MonoBehaviour
{
    public GameObject audioSettingsMenu;

    public AudioMixer audioMixer;
    public AudioSource sliderClickNoise;

    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;

    public void Start()
    {
        // set effects slider to the audio mixer's value
        float musicVolume = .5f;
        audioMixer.GetFloat("musicVolume", out musicVolume);
        musicVolumeSlider.value = musicVolume;

        // set effects slider to the audio mixer's value
        float effectsVolume = .5f;
        audioMixer.GetFloat("effectsVolume", out effectsVolume);
        effectsVolumeSlider.value = effectsVolume;
    }

    public void SetMusicVolume (float musicVolume)
    {
        audioMixer.SetFloat("musicVolume", musicVolume);
        sliderClickNoise.Play();
    }

    public void SetEffectsVolume(float effectsVolume)
    {
        audioMixer.SetFloat("effectsVolume", effectsVolume);
        sliderClickNoise.Play();
    }

    public void CloseSettingsMenu() {
        audioSettingsMenu.SetActive(false);
    }

}
