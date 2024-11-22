using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManagerUI : MonoBehaviour
{
    public int score { get; private set; }
    public int zombieCount { get; private set; }

    public Text scoreText;
    public Text zombieCountText;
    public Text rewardNotificationText;

    private float notificationTime = 2f;

    void Start()
    {
        UpdateUI();
        rewardNotificationText.gameObject.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Điểm: " + score;
    }

    public void AddZombieCount()
    {
        zombieCount++;
        zombieCountText.text = "Zombie đã tiêu diệt: " + zombieCount;
        ShowRewardNotification();
    }

    private void ShowRewardNotification()
    {
        StartCoroutine(DisplayNotification());
    }

    private IEnumerator DisplayNotification()
    {
        rewardNotificationText.gameObject.SetActive(true);
        yield return new WaitForSeconds(notificationTime);
        rewardNotificationText.gameObject.SetActive(false);
    }
}