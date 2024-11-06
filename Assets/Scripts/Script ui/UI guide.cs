using UnityEngine;
using UnityEngine.UI;

public class GameGuideManager : MonoBehaviour
{
    public GameObject guidePanel;           // Panel chứa hướng dẫn
    public Text guideText;                  // Text hiển thị nội dung hướng dẫn

    private void Start()
    {
        guidePanel.SetActive(false);         // Ẩn hướng dẫn khi bắt đầu
    }

    public void ShowGuide()
    {
        guidePanel.SetActive(true);          // Hiện hướng dẫn
        guideText.text = GetGuideText();    // Cập nhật nội dung hướng dẫn
    }

    public void HideGuide()
    {
        guidePanel.SetActive(false);         // Ẩn hướng dẫn
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