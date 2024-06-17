using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenuController : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        // Set slider values from saved preferences
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1.0f);

        // Add listeners to handle slider value changes
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetBGMVolume(float value)
    {
        AudioManager.Instance.SetBGMVolume(value);
    }

    private void SetSFXVolume(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
    }
}
