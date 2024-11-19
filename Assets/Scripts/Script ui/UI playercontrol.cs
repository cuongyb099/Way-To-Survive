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
    private const float buffDuration = 5f; // Thời gian buff có hiệu lực

    private readonly Dictionary<string, System.Action> buffs = new Dictionary<string, System.Action>
    {
        { "Tăng Tốc Độ", () => ApplyBuff(ref moveSpeed, 1.5f) },
        { "Tăng Máu", () => ApplyBuff(ref maxHealth, 20) },
        { "Tỉ Lệ Bạo Kích", () => ApplyBuff(ref critChance, 0.1f) }
    };

    private void Start()
    {
        buffPanel.SetActive(false);
        activateBuffButton.onClick.AddListener(ActivateBuff);
    }

    public void ShowBuff(string buffName)
    {
        if (buffs.ContainsKey(buffName))
        {
            currentBuff = buffName;
            buffNameText.text = $"Buff: {currentBuff}";
            buffPanel.SetActive(true);
        }
    }

    private void ActivateBuff()
    {
        if (!string.IsNullOrEmpty(currentBuff))
        {
            buffs[currentBuff].Invoke();
            HideBuff();
        }
    }

    private static IEnumerator ApplyBuff<T>(ref T stat, T increment)
    {
        stat = (dynamic)stat * (dynamic)increment; // Áp dụng buff
        yield return new WaitForSeconds(buffDuration);
        stat = (dynamic)stat / (dynamic)increment; // Khôi phục buff
    }

    public void HideBuff()
    {
        buffPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ShowBuff("Tăng Tốc Độ");
        else if (Input.GetKeyDown(KeyCode.Alpha2)) ShowBuff("Tăng Máu");
        else if (Input.GetKeyDown(KeyCode.Alpha3)) ShowBuff("Tỉ Lệ Bạo Kích");
    }
}