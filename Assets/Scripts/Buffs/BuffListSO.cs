using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "BuffListSO", menuName = "Item/Buff/new BuffListSO")]
public class BuffListSO : ScriptableObject
{
	[SerializedDictionary("Rarity", "Buffs")]
	[SerializeField] public SerializedDictionary<BuffRarity, ListOfBuffTypeSO> Buffs = new();
	[SerializedDictionary("Rarity", "Data")]
	[SerializeField] public SerializedDictionary<BuffRarity, BuffCardUI> BuffRarityCard = new();
	[SerializedDictionary("Rarity", "Rate")]
	[SerializeField] public SerializedDictionary<BuffRarity, float> BuffRarityRate = new();
	public BuffSO ChoseRandomRarityBuff()
    {
        float curSum = 0;
        float rdnNum = Random.value* calSumNumber();
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
	public List<BuffSO> ChoseRandomBuffAmmount(int n)
	{
        List<BuffSO> l = new List<BuffSO>();
        while (l.Count < n)
        {
            BuffSO t = ChoseRandomRarityBuff();
            if (!l.Contains(t))
            {
                l.Add(t);
            }
		}
        return l;
	}
}
