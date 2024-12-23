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
public class BaseBuffSO : StatusEffectSO
{
    [Header("Buff Data")]
	public BuffRarity RareType;

	public override BaseStatusEffect AddStatusEffect(StatsController controller)
	{
		return null;
	}
	public virtual object[] GetValues()
	{
		return null;
	}
}
