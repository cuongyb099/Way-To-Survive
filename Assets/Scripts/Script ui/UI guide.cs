using UnityEngine;
using UnityEngine.UI;

public class GameGuideManager : MonoBehaviour
{
    [SerializeField] private GameObject guidePanel;   // Panel chứa hướng dẫn
    [SerializeField] private Text guideText;           // Text hiển thị nội dung hướng dẫn

    private void Start()
    {
        HideGuide();  // Ẩn hướng dẫn khi bắt đầu
    }

    public void ToggleGuide()
    {
        if (guidePanel.activeSelf)
            HideGuide();
        else
            ShowGuide();
    }

    public void ShowGuide()
    {
        guidePanel.SetActive(true);
        guideText.text = GetGuideText();
    }

    public void HideGuide()
    {
        guidePanel.SetActive(false);
    }

    private string GetGuideText()
    {
        return "Hướng Dẫn Chơi:\n\n" +
               "Điều Khiển:\n" +
               "- W: Tiến\n" +
               "- A: Sang trái\n" +
               "- S: Lùi\n" +
               "- D: Sang phải\n" +
               "- Space: Nhảy\n\n" +
               "Kỹ Năng:\n" +
               "- Sử dụng kỹ năng để tăng cường sức mạnh.\n" +
               "- Hãy chú ý tới thời gian hồi chiêu.\n\n" +
               "Mẹo:\n" +
               "- Tìm kiếm vật phẩm để tăng khả năng chiến đấu.\n" +
               "- Thực hành để làm quen với điều khiển.";
    }
}