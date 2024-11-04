using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManagerUI : MonoBehaviour
{
    [Header("Score Variables")]
    public int score = 0;
    public int zombieCount = 0;

    [Header("UI References")]
    public Text scoreText;
    public Text zombieCountText;
    public Text rewardNotificationText;

    private float notificationTime = 2f;

    void Start()
    {
        UpdateUI();
        rewardNotificationText.gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        scoreText.text = $"Điểm: {score}";
        zombieCountText.text = $"Zombie đã tiêu diệt: {zombieCount}";
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void AddZombieCount()
    {
        zombieCount++;
        UpdateUI();
        ShowRewardNotification();
    }

    private void ShowRewardNotification()
    {
        StartCoroutine(DisplayNotification());
    }

    
}