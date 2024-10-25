using System.Collections;
using System.Collections.Generic;
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

    public override void StartStatus(StatsController controller)
    {
        base.StartStatus(controller);
        controller.AddModifier(StatType, new StatModifier(Value, ModifierType));
    }
    public override void EndStatus(StatsController controller)
    {
        base.EndStatus(controller);
        controller.RemoveModifier(StatType, new StatModifier(Value, ModifierType));

    }
}
