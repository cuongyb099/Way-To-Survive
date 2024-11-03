using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManagerUI : MonoBehaviour
{
    public int score = 0;                  // Điểm số hiện tại
    public int zombieCount = 0;            // Số zombie đã tiêu diệt
    public Text scoreText;                 // Tham chiếu đến Text điểm số
    public Text zombieCountText;           // Tham chiếu đến Text số zombie
    public Text killNotificationText;      // Tham chiếu đến Text thông báo
    private float notificationTime = 2f;   // Thời gian hiển thị thông báo

    void Start()
    {
        UpdateScoreText();                 // Cập nhật điểm số lúc bắt đầu
        UpdateZombieCountText();           // Cập nhật số zombie lúc bắt đầu
        killNotificationText.gameObject.SetActive(false); // Tắt thông báo
    }

    // Phương thức thêm điểm
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Cập nhật UI điểm số
    private void UpdateScoreText()
    {
        scoreText.text = "Điểm: " + score.ToString();
    }

    // Phương thức tăng số zombie đã tiêu diệt
    public void AddZombieCount()
    {
        zombieCount++;
        UpdateZombieCountText();
        ShowKillNotification(); // Hiển thị thông báo
    }

    // Cập nhật UI số zombie đã tiêu diệt
    private void UpdateZombieCountText()
    {
        zombieCountText.text = "Zombie đã tiêu diệt: " + zombieCount.ToString();
    }

    // Hiển thị thông báo tiêu diệt
    private void ShowKillNotification()
    {
        StartCoroutine(DisplayNotification());
    }

    private IEnumerator DisplayNotification()
    {
        killNotificationText.gameObject.SetActive(true); // Bật thông báo
        yield return new WaitForSeconds(notificationTime); // Chờ trong thời gian quy định
        killNotificationText.gameObject.SetActive(false); // Tắt thông báo
    }
}