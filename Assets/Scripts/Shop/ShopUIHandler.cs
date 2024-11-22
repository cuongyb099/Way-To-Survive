using TMPro;
using UnityEngine;

public class ShopUIHandler : CanvasUIHandler
{
    public TextMeshProUGUI CashText;
    public GameObject BuffPanel;

    public int BuffPrice = 3;
    private PlayerController player;

    private void Awake()
    {
        player = GameManager.Instance.Player;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        PlayerEvent.OnCashChange += UpdateCashText;
        UpdateCashText(player.Cash);
    }

    protected override void OnDisable()
    {
        base.OnDisable();  
        PlayerEvent.OnCashChange -= UpdateCashText;
    }

    private void UpdateCashText(int amount)
    {
        CashText.text = $"<color=#3ECDFF>Cash:</color> {amount}";
    }

    public void OnBuyBuff()
    {
        if (player.Cash < BuffPrice)
        {
            Debug.Log("Not enough cash");
            return;
        }
        player.Cash -= BuffPrice;
        BuffPanel.SetActive(true);
    }
}
