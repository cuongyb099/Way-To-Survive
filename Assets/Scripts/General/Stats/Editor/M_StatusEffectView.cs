
public class M_StatusEffectView : M_ItemView
{
	private const string _statusEffect = "Status Effect";

	public M_StatusEffectView(StatsController stats) : base(stats)
	{
	}

	protected override void SetTitle()
	{
		title = _statusEffect;
	}
}
