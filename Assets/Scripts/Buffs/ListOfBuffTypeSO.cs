using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "SpecificListSO", menuName = "Item/Buff/new SpecificListSO")]
public class ListOfBuffTypeSO : ScriptableObject
{
    public List<BuffSO> Buffs;
    public BuffSO ChooseRandomBuff()
    {
        return Buffs[Random.Range(0, Buffs.Count)];
	}
}
