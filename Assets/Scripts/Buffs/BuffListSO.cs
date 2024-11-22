using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using System;
using System.Threading;

[CreateAssetMenu(fileName = "BuffListSO", menuName = "Item/Buff/new BuffListSO")]
public class BuffListSO : ScriptableObject
{
	[SerializedDictionary("Rarity", "Buffs")]
	[SerializeField] public SerializedDictionary<BuffRarity, ListOfBuffTypeSO> Buffs = new();
	[SerializedDictionary("Rarity", "Data")]
	[SerializeField] public SerializedDictionary<BuffRarity, BuffCardUI> BuffRarityCard = new();
	[SerializedDictionary("Rarity", "Rate")]
	[SerializeField] public SerializedDictionary<BuffRarity, float> BuffRarityRate = new();
	public BasicBuffSO ChoseRandomRarityBuff()
    {
        float curSum = 0;
        float rdnNum = UnityEngine.Random.value* calSumNumber();
		foreach (var key in BuffRarityRate.Keys)
		{
            curSum += BuffRarityRate[key];
            if (rdnNum > curSum) continue;
            return Buffs[key].ChooseRandomBuff();
		}
        return null;
    }
    private float calSumNumber()
    {
        float sum = 0;
        foreach(var key in BuffRarityRate.Keys)
        {
            sum += BuffRarityRate[key];
        }
        return sum;
    }
	public List<BasicBuffSO> ChoseRandomBuffAmmount(int n)
	{
        List<BasicBuffSO> l = new List<BasicBuffSO>();
        int count = 0;
        while (l.Count < n && count <999)
        {
            count++;
            BasicBuffSO t = ChoseRandomRarityBuff();
            if (!l.Contains(t))
            {
                if (!t.Stackable && GameManager.Instance.Player.BuffList.Contains(t.ID)) continue;
                l.Add(t);
            }
        }

        return l;
	}
}
