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

    private BuffManager buffManager;

    private void Start()
    {
        buffManager = new BuffManager();
        buffPanel.SetActive(false);
        activateBuffButton.onClick.AddListener(ActivateBuff);
    }

    public void ShowBuff(string buffName)
    {
        buffNameText.text = $"Buff: {buffName}";
        buffPanel.SetActive(true);
    }

    private void ActivateBuff()
    {
        // Tạo các buff
        if (buffNameText.text == "Buff: Tăng Tốc Độ")
        {
            Buff speedBuff = new Buff("Tăng Tốc Độ", 5f,
                () => moveSpeed *= 1.5f,
                () => moveSpeed /= 1.5f);
            buffManager.ApplyBuff(speedBuff);
        }
        else if (buffNameText.text == "Buff: Tăng Máu")
        {
            Buff healthBuff = new Buff("Tăng Máu", 5f,
                () => maxHealth += 20,
                () => maxHealth -= 20);
            buffManager.ApplyBuff(healthBuff);
        }
        else if (buffNameText.text == "Buff: Tỉ Lệ Bạo Kích")
        {
            Buff critBuff = new Buff("Tỉ Lệ Bạo Kích", 5f,
                () => critChance += 0.1f,
                () => critChance -= 0.1f);
            buffManager.ApplyBuff(critBuff);
        }
        HideBuff();
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

    // Lớp Buff
    [System.Serializable]
    public class Buff
    {
        public string buffName;
        public float duration;
        public System.Action applyBuff;
        public System.Action revertBuff;

        public Buff(string name, float duration, System.Action apply, System.Action revert)
        {
            this.buffName = name;
            this.duration = duration;
            this.applyBuff = apply;
            this.revertBuff = revert;
        }
    }

    // Lớp BuffManager
    public class BuffManager : MonoBehaviour
    {
        private List<Buff> activeBuffs = new List<Buff>();

        public void ApplyBuff(Buff buff)
        {
            if (activeBuffs.Contains(buff)) return; // Kiểm tra nếu buff đã tồn tại

            activeBuffs.Add(buff);
            buff.applyBuff.Invoke();
            Coroutine coroutine = StartCoroutine(RemoveBuffAfterDuration(buff));
            // Khởi tạo coroutine để quản lý thời gian buff
        }

        private IEnumerator RemoveBuffAfterDuration(Buff buff)
        {
            yield return new WaitForSeconds(buff.duration);
            buff.revertBuff.Invoke();
            activeBuffs.Remove(buff);
        }
    }
}