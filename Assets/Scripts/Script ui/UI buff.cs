using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject buffPanel;
    [SerializeField] private Text buffNameText;
    [SerializeField] private Button activateBuffButton;

    [Header("Player Stats")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float critChance = 0.1f; // Tỉ lệ bạo kích (10%)

    private string currentBuff;
    private float buffDuration = 5f; // Thời gian buff có hiệu lực

    private Dictionary<string, System.Action> buffs;

    private void Start()
    {
        buffPanel.SetActive(false);
        activateBuffButton.onClick.AddListener(ActivateBuff);

        // Định nghĩa các buff và hành động tương ứng
        buffs = new Dictionary<string, System.Action>
        {
            { "Tăng Tốc Độ", () => StartCoroutine(ApplySpeedBuff()) },
            { "Tăng Máu", () => StartCoroutine(ApplyHealthBuff()) },
            { "Tỉ Lệ Bạo Kích", () => StartCoroutine(ApplyCritChanceBuff()) }
        };
    }

    public void ShowBuff(string buffName)
    {
        currentBuff = buffName;
        buffNameText.text = $"Buff: {currentBuff}";
        buffPanel.SetActive(true);
    }

    private void ActivateBuff()
    {
        if (!string.IsNullOrEmpty(currentBuff) && buffs.ContainsKey(currentBuff))
        {
            buffs[currentBuff].Invoke();
            HideBuff();
        }
    }

    private IEnumerator ApplySpeedBuff()
    {
        yield return ApplyBuff(() => moveSpeed *= 1.5f, () => moveSpeed /= 1.5f);
    }

    private IEnumerator ApplyHealthBuff()
    {
        yield return ApplyBuff(() => maxHealth += 20, () => maxHealth -= 20);
    }

    private IEnumerator ApplyCritChanceBuff()
    {
        yield return ApplyBuff(() => critChance += 0.1f, () => critChance -= 0.1f);
    }

    private IEnumerator ApplyBuff(System.Action apply, System.Action revert)
    {
        apply.Invoke(); // Áp dụng buff
        yield return new WaitForSeconds(buffDuration);
        revert.Invoke(); // Khôi phục buff
    }

    public void HideBuff()
    {
        buffPanel.SetActive(false);
    }

    private void Update()
    {
        // Kiểm tra đầu vào để kích hoạt buff
        if (Input.GetKeyDown(KeyCode.Alpha1)) ShowBuff("Tăng Tốc Độ");
        else if (Input.GetKeyDown(KeyCode.Alpha2)) ShowBuff("Tăng Máu");
        else if (Input.GetKeyDown(KeyCode.Alpha3)) ShowBuff("Tỉ Lệ Bạo Kích");
    }
}