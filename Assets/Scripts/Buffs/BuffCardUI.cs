using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffCardUI : MonoBehaviour
{
    public TextMeshProUGUI Name;
	public TextMeshProUGUI Description;
	public Image Icon;
	public Button Button;
	private BuffSO buffData;
	public void Initialize(BuffSO buff)
    {
        buffData = buff;
        Name.text = buff.BuffName;
		Description.text = buff.BuffDescription;
		Icon.sprite = buff.BuffIcon;
	}
	public void BuffUpPlayer()
    {
        GameManager.Instance.Player.AddBuffToPlayer(buffData);
    }
}
