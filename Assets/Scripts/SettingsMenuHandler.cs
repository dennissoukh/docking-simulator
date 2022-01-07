using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenuHandler : MonoBehaviour
{
  private Canvas menu;
  private CanvasGroup group;
  public AudioMixer audioMixer;
  public Slider volumeSlider;
  float currentVolume;

  public AudioMixer mixer;

  public void SetVolume(float sliderValue) {
    mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
  }

  public void SaveSettings()
  {
    PlayerPrefs.SetFloat("VolumePreference", currentVolume);
  }
  // Start is called before the first frame update
  void Awake()
  {
    menu = GetComponentInChildren<Canvas>();
    group = GetComponentInChildren<CanvasGroup>();

    // By default, keep the settings menu hidden
    menu.enabled = false;
  }
}
