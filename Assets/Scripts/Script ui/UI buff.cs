using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject buffPanel;
    [SerializeField] private Text buffNameText;
    [SerializeField] private Button activateBuffButton;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float critChance = 0.1f; // Tỉ lệ bạo kích (10%)

    private string currentBuff;
    private float buffDuration = 5f; // Thời gian buff có hiệu lực

    private void Start()
    {
        buffPanel.SetActive(false);
        activateBuffButton.onClick.AddListener(ActivateBuff);
    }

    public void ShowBuff(string buffName)
    {
        currentBuff = buffName;
        buffNameText.text = $"Buff: {currentBuff}";
        buffPanel.SetActive(true);
    }

    private void ActivateBuff()
    {
        if (!string.IsNullOrEmpty(currentBuff))
        {
            switch (currentBuff)
            {
                case "Tăng Tốc Độ":
                    StartCoroutine(ApplySpeedBuff());
                    break;
                case "Tăng Máu":
                    StartCoroutine(ApplyHealthBuff());
                    break;
                case "Tỉ Lệ Bạo Kích":
                    StartCoroutine(ApplyCritChanceBuff());
                    break;
            }
            HideBuff();
        }
    }

    private IEnumerator ApplySpeedBuff()
    {
        moveSpeed *= 1.5f; // Tăng 50% tốc độ
        yield return new WaitForSeconds(buffDuration);
        moveSpeed /= 1.5f; // Khôi phục tốc độ
    }

    private IEnumerator ApplyHealthBuff()
    {
        maxHealth += 20; // Tăng 20 máu tối đa
        yield return new WaitForSeconds(buffDuration);
        maxHealth -= 20; // Khôi phục máu tối đa
    }

    private IEnumerator ApplyCritChanceBuff()
    {
        critChance += 0.1f; // Tăng 10% tỉ lệ bạo kích
        yield return new WaitForSeconds(buffDuration);
        critChance -= 0.1f; // Khôi phục tỉ lệ bạo kích
    }

    public void HideBuff()
    {
        buffPanel.SetActive(false);
    }

    private void Update()
    {
        // Kiểm tra đầu vào để kích hoạt buff cho ví dụ
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Nhấn phím 1 để tăng tốc độ
        {
            ShowBuff("Tăng Tốc Độ");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Nhấn phím 2 để tăng máu
        {
            ShowBuff("Tăng Máu");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) // Nhấn phím 3 để tăng tỉ lệ bạo kích
        {
            ShowBuff("Tỉ Lệ Bạo Kích");
        }
    }
}