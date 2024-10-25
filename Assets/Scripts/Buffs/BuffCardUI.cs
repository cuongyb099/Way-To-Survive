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
	private BasicBuffSO buffData;
	public void Initialize(BasicBuffSO buff)
    {
        buffData = buff;
        Name.text = buff.Name;
		Description.text = GetDescription(buff);
		Icon.sprite = buff.Icon;
	}
	public void BuffUpPlayer()
    {
        GameManager.Instance.Player.AddBuffToPlayer(buffData);
    }
	private string GetDescription(BasicBuffSO buff)
	{
		string temp = buff.Description.Replace("{value}", buff.Value.ToString() + ((buff.ModifierType is StatModType.Percentage)?"%":""));
		temp = temp.Replace("{pvalue}", (buff.Value*100f).ToString()+"%");
		return temp;
	}
}
