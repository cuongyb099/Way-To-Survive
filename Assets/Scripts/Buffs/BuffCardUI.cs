using System;
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
		Description.text = GetDescription(buff);
		Icon.sprite = buff.BuffIcon;
	}
	public void BuffUpPlayer()
    {
        GameManager.Instance.Player.AddBuffToPlayer(buffData);
    }
	private string GetDescription(BuffSO buff)
	{
		string temp = buff.BuffDescription.Replace("{value}", buff.Value.ToString() + ((buff.ModifierType is StatModType.Percentage)?"%":""));
		temp = temp.Replace("{pvalue}", (buff.Value*100f).ToString()+"%");
		return temp;
	}
}
