using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public enum BuffRarity
{
	Common,
	Uncommon,
	Rare,
	ExtremelyRare,
	Myth,
}
[CreateAssetMenu(fileName = "BuffSO", menuName = "Item/Buff/new BuffSO")]
public class BasicBuffSO : StatusEffectSO
{
    [Header("Basic Buff Data")]
	public BuffRarity RareType;
	public StatType StatType;
    public StatModType ModifierType;
	public float Value;

	public override BuffStatusEffect AddStatusEffect(StatsController controller)
	{
		BuffStatusEffect buffEffect = new BuffStatusEffect(this, controller);
		controller.ApplyEffect(buffEffect);
		return buffEffect;
	}
}
