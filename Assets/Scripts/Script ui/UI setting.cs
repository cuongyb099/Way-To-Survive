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
        // Thiết lập giá trị ban đầu cho slider
        soundSlider.value = PlayerPrefs.GetFloat(SoundVolumeKey, 1f); // Mặc định là 1 (max)
        brightnessSlider.value = PlayerPrefs.GetFloat(BrightnessKey, 1f); // Mặc định là 1 (max)

        // Gán sự kiện cho slider và nút
        soundSlider.onValueChanged.AddListener(UpdateSoundVolume);
        brightnessSlider.onValueChanged.AddListener(UpdateBrightness);
        saveButton.onClick.AddListener(SaveOptions);
    }

    private void UpdateSoundVolume(float value)
    {
        AudioListener.volume = value; // Cập nhật âm thanh
    }

    private void UpdateBrightness(float value)
    {
        RenderSettings.ambientIntensity = value; // Cập nhật độ sáng
    }

    private void SaveOptions()
    {
        PlayerPrefs.SetFloat(SoundVolumeKey, soundSlider.value);
        PlayerPrefs.SetFloat(BrightnessKey, brightnessSlider.value);
        PlayerPrefs.Save(); // Lưu vào ổ đĩa
    }

    private void OnDestroy()
    {
        // Xóa sự kiện khi đối tượng bị hủy
        soundSlider.onValueChanged.RemoveListener(UpdateSoundVolume);
        brightnessSlider.onValueChanged.RemoveListener(UpdateBrightness);
        saveButton.onClick.RemoveListener(SaveOptions);
    }
}