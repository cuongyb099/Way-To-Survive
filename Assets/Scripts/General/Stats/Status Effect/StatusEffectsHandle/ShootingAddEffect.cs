using System;
using System.Collections;
using UnityEngine;

public class ShootingAddEffect : BuffStatusEffect
{

	private int dem;
    public ShootingAddBuffSO DataReal { get { return Data as ShootingAddBuffSO; } }
	public ShootingAddEffect(ShootingAddBuffSO data, StatsController target, Action OnStart = null, Action onEnd = null, Action onActive = null) : base(data, target, OnStart, onEnd, onActive)
    {

    }
	protected override void HandleStart()
    {
		PlayerEvent.OnShoot += AddSpeed;
	}
    protected override void HandleOnUpdate()
    {

    }
    protected override void HandleOnEnd()
    {
		PlayerEvent.OnShoot -= AddSpeed;
	}

    public override void HandleStackChange(StackStatus stackStatus)
    {
        
    }
	public void AddSpeed()
	{
		dem++;

		if (dem == DataReal.BuffAfter)
		{
			dem = 0;
			stats.AddModifier(DataReal.StatType, new StatModifier(DataReal.Value, DataReal.ModifierType));
		}
	}
}