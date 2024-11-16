using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Button saveButton;

    private void Start()
    {
        // Thiết lập giá trị ban đầu cho slider
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f); // Mặc định là 1 (max)
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 1f); // Mặc định là 1 (max)

        // Gán sự kiện cho slider và nút
        soundSlider.onValueChanged.AddListener(SetSoundVolume);
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        saveButton.onClick.AddListener(SaveOptions);
    }

    private void SetSoundVolume(float value)
    {
        // Cập nhật âm thanh (giả sử bạn có một AudioManager)
        AudioListener.volume = value; // Cập nhật âm thanh
    }

    private void SetBrightness(float value)
    {
        // Cập nhật độ sáng (giả sử bạn đang sử dụng một ánh sáng môi trường)
        RenderSettings.ambientIntensity = value; // Cập nhật độ sáng
    }

    private void SaveOptions()
    {
        // Lưu tùy chọn vào PlayerPrefs
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
        PlayerPrefs.SetFloat("Brightness", brightnessSlider.value);
        PlayerPrefs.Save(); // Lưu vào ổ đĩa
    }

    private void OnDestroy()
    {
        // Xóa sự kiện khi đối tượng bị hủy
        soundSlider.onValueChanged.RemoveListener(SetSoundVolume);
        brightnessSlider.onValueChanged.RemoveListener(SetBrightness);
        saveButton.onClick.RemoveListener(SaveOptions);
    }
}