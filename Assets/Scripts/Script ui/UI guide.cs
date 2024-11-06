using UnityEngine;
using UnityEngine.UI;

public class GameGuideManager : MonoBehaviour
{
    [SerializeField] private GameObject guidePanel;
    [SerializeField] private Text guideText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;

    private string[] guideSteps;
    private int currentStepIndex = 0;

    private void Start()
    {
        guideSteps = new string[]
        {
            "Hướng Dẫn Chơi:\n\nĐiều Khiển:\n- W: Tiến\n- A: Sang trái\n- S: Lùi\n- D: Sang phải\n- Space: Nhảy",
            "Kỹ Năng:\n- Sử dụng kỹ năng để tăng cường sức mạnh.\n- Hãy chú ý tới thời gian hồi chiêu.",
            "Mẹo:\n- Tìm kiếm vật phẩm để tăng khả năng chiến đấu.\n- Thực hành để làm quen với điều khiển."
        };

        HideGuide();
        nextButton.onClick.AddListener(NextStep);
        previousButton.onClick.AddListener(PreviousStep);
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
        currentStepIndex = 0;
        UpdateGuideText();
    }

    public void HideGuide()
    {
        guidePanel.SetActive(false);
    }

    private void UpdateGuideText()
    {
        guideText.text = guideSteps[currentStepIndex];
        previousButton.interactable = currentStepIndex > 0;
        nextButton.interactable = currentStepIndex < guideSteps.Length - 1;
    }

    private void NextStep()
    {
        if (currentStepIndex < guideSteps.Length - 1)
        {
            currentStepIndex++;
            UpdateGuideText();
        }
    }

    private void PreviousStep()
    {
        if (currentStepIndex > 0)
        {
            currentStepIndex--;
            UpdateGuideText();
        }
    }
}