using DG.Tweening;

public class BuffEffect : BaseStatusEffect
{
	public BuffSO data;
	public BuffEffect(BuffSO buffData, StatsController target) : base(buffData.HasDuration, target, buffData.Duration)
	{
		type = StatusEffectType.Positive;
		data = buffData;
		StartBuff();
	}

	protected void StartBuff()
	{
		if(!stats.TryGetStat(data.StatType, out Stat stat)) return;
		stat.AddModifier(new StatModifier(data.Value,data.ModifierType));
	}
	protected override void HandleEnd()
	{
		if (!stats.TryGetStat(data.StatType, out Stat stat)) return;
		stat.RemoveModifier(new StatModifier(data.Value, data.ModifierType));
	}

}
