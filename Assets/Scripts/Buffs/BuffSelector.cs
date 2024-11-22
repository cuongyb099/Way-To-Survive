using BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class BuffSelector : MonoBehaviour
{
    public BuffListSO BuffsData;
	public int CardCount = 3;
	private List<BuffCardUI> Cards;
	private void Awake()
	{
		Cards = new List<BuffCardUI>();
	}
	public void OnEnable()
	{
		InitializeAll();
	}
	public void OnDisable()
	{
		for(int i = Cards.Count - 1; i >= 0; i--)
		{
			BuffCardUI card = Cards[i];
			Cards.RemoveAt(i);
			card.Button.onClick.RemoveListener(DeactivateParent);
			Destroy(card.gameObject);
		}
	}
	public void InitializeAll()
	{
		List<BasicBuffSO> buffs = BuffsData.ChoseRandomBuffAmmount(CardCount);
		for (int i = 0; i < buffs.Count; i++)
		{
			BuffCardUI card = Instantiate(BuffsData.BuffRarityCard[buffs[i].RareType],transform);
			card.Initialize(buffs[i]);
			Cards.Add(card);
			card.Button.onClick.AddListener(DeactivateParent);
		}
	}
	public void DeactivateParent()
	{
		transform.parent.parent.gameObject.SetActive(false);
	}
}
