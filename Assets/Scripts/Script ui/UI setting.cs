using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Button saveButton;

    private const string SoundVolumeKey = "SoundVolume";
    private const string BrightnessKey = "Brightness";

    private void Start()
    {
        LoadSettings();
        AssignListeners();
    }

    private void LoadSettings()
    {
        soundSlider.value = PlayerPrefs.GetFloat(SoundVolumeKey, 1f);
        brightnessSlider.value = PlayerPrefs.GetFloat(BrightnessKey, 1f);
    }

    private void AssignListeners()
    {
        soundSlider.onValueChanged.AddListener(UpdateSoundVolume);
        brightnessSlider.onValueChanged.AddListener(UpdateBrightness);
        saveButton.onClick.AddListener(SaveOptions);
    }

    private void UpdateSoundVolume(float value)
    {
        AudioListener.volume = value;
    }

    private void UpdateBrightness(float value)
    {
        RenderSettings.ambientIntensity = value;
    }

    private void SaveOptions()
    {
        PlayerPrefs.SetFloat(SoundVolumeKey, soundSlider.value);
        PlayerPrefs.SetFloat(BrightnessKey, brightnessSlider.value);
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
        saveButton.onClick.RemoveListener(SaveOptions);
    }
}