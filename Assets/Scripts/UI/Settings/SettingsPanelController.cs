using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Utilities;

public class SettingsPanelController : MonoBehaviour
{
    // Default values
    private const bool DefaultFullscreen = false;
    private const bool DefaultVSync = false;
    private const float DefaultMasterVolume = 0.5f;
    private const float DefaultMusicVolume = 0.5f;
    private const float DefaultSFXVolume = 0.5f;
    private const float DefaultMouseSensitivity = 0.5f;

    public Toggle fullscreenToggle;
    public Toggle vSyncToggle;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider mouseSensitivitySlider;

    public AudioMixer audioMixer; 

    private void Start()
    {
        LoadSettings();
    }

    public void LoadSettings()
    {
        // Load settings or set to defaults if not previously saved
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", DefaultFullscreen ? 1 : 0) == 1;
        vSyncToggle.isOn = PlayerPrefs.GetInt("VSync", DefaultVSync ? 1 : 0) == 1;
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", DefaultMasterVolume);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", DefaultMusicVolume);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", DefaultSFXVolume);
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", DefaultMouseSensitivity);

        ApplySettings(); // Apply loaded or default settings
    }

    public void SaveSettings()
    {
        // Save current settings
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("VSync", vSyncToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivitySlider.value);

        PlayerPrefs.Save(); // Important to save the changes!
    }

    public void ResetSettings()
    {
        // Reset to default settings
        fullscreenToggle.isOn = DefaultFullscreen;
        vSyncToggle.isOn = DefaultVSync;
        masterVolumeSlider.value = DefaultMasterVolume;
        musicVolumeSlider.value = DefaultMusicVolume;
        sfxVolumeSlider.value = DefaultSFXVolume;
        mouseSensitivitySlider.value = DefaultMouseSensitivity;

        ApplySettings(); // Apply and save default settings
        SaveSettings();
    }

    void ApplySettings()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        QualitySettings.vSyncCount = vSyncToggle.isOn ? 1 : 0;
        //audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolumeSlider.value) * 20);
        //audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolumeSlider.value) * 20);
        //audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolumeSlider.value) * 20);
        
    }
    
    public void OnFullscreenToggleChanged()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        ApplySettings();
        SaveSettings();
    }

    public void OnVSyncToggleChanged()
    {
        QualitySettings.vSyncCount = vSyncToggle.isOn ? 1 : 0;
        ApplySettings();
        SaveSettings();
    }
    
    public void OnMasterVolumeChanged()
    {
        //audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolumeSlider.value) * 20);
        ApplySettings();
        SaveSettings();
    }

    public void OnMusicVolumeChanged()
    {
        //audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolumeSlider.value) * 20);
        ApplySettings();
        SaveSettings();
    }

    public void OnSFXVolumeChanged()
    {
        //audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolumeSlider.value) * 20);
        ApplySettings();
        SaveSettings();
    }

    public void OnMouseSensitivityChanged()
    {
        // This might be used directly in input handling or we might just save it
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivitySlider.value);
        ApplySettings();
        SaveSettings();
    }

    public void OnOKButtonClicked()
    {
        // save the changes too
        UIManager.Instance.OpenMainMenu();
    }
    
    public void OnReturnButtonClicked()
    {
        UIManager.Instance.OpenMainMenu();
    }
    
}
    

