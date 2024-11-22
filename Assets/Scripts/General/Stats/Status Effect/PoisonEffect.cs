using DG.Tweening;

public class PoisonEffect : BaseStatusEffect
{
	protected float eachTimeDealDamage;

	public PoisonEffect(bool hasDuration, StatsController target, float duration = 0) : base(hasDuration, target, duration)
	{
		type = StatusEffectType.Negative;
	}

	private Tween _poisonTween;

	protected override void HandleStart()
	{
		if(!stats.TryGetAttribute(AttributeType.Hp, out Attribute attribute)) return;

		_poisonTween = DOVirtual.DelayedCall(1f, () =>
		{
			attribute.Value -= 3f;
		}).SetLoops(-1, LoopType.Restart);
	}

	protected override void HandleEnd()
	{
		_poisonTween.Kill();
	}
}
