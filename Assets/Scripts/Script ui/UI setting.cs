using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Button saveButton;

    private const string SoundVolumeKey = "SoundVolume";
    private const string BrightnessKey = "Brightness";
    private const string FullscreenKey = "Fullscreen";

    private void Start()
    {
        LoadSettings();
        AssignListeners();
    }

    private void LoadSettings()
    {
        soundSlider.value = PlayerPrefs.GetFloat(SoundVolumeKey, 1f);
        brightnessSlider.value = PlayerPrefs.GetFloat(BrightnessKey, 1f);
        fullscreenToggle.isOn = PlayerPrefs.GetInt(FullscreenKey, 1) == 1;
    }

    private void AssignListeners()
    {
        soundSlider.onValueChanged.AddListener(UpdateSoundVolume);
        brightnessSlider.onValueChanged.AddListener(UpdateBrightness);
        fullscreenToggle.onValueChanged.AddListener(ToggleFullscreen);
        saveButton.onClick.AddListener(SaveSettings);
    }

    private void UpdateSoundVolume(float value)
    {
        AudioListener.volume = value;
    }

    private void UpdateBrightness(float value)
    {
        RenderSettings.ambientIntensity = value;
    }

    private void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat(SoundVolumeKey, soundSlider.value);
        PlayerPrefs.SetFloat(BrightnessKey, brightnessSlider.value);
        PlayerPrefs.SetInt(FullscreenKey, fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void RemoveListeners()
    {
        soundSlider.onValueChanged.RemoveListener(UpdateSoundVolume);
        brightnessSlider.onValueChanged.RemoveListener(UpdateBrightness);
        fullscreenToggle.onValueChanged.RemoveListener(ToggleFullscreen);
        saveButton.onClick.RemoveListener(SaveSettings);
    }
}