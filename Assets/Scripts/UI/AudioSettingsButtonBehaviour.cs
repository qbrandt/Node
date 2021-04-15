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
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);

        // set effects slider to the audio mixer's value
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
    }

    public void SetMusicVolume ()
    {
        float sliderValue = musicVolumeSlider.value;
        audioMixer.SetFloat("musicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetEffectsVolume()
    {
        float sliderValue = effectsVolumeSlider.value;
        audioMixer.SetFloat("effectsVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("EffectsVolume", sliderValue);
    }

    public void OpenSettingsMenu()
    {
        audioSettingsMenu.SetActive(true);
    }

    public void CloseSettingsMenu() {
        audioSettingsMenu.SetActive(false);
    }

}
