using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;              // Điểm số hiện tại
    public Text scoreText;             // Text hiển thị điểm số

    void Start()
    {
        UpdateScoreText();             // Cập nhật điểm số lúc bắt đầu
    }

    public void AddScore(int amount)
    {
        score += amount;                // Thêm điểm
        UpdateScoreText();             // Cập nhật UI
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Điểm: " + score.ToString(); // Cập nhật text hiển thị
    }
}