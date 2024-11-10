using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAddEffect : BuffStatusEffect
{
	private int dem;
	private List<BuffStatusEffect> effects = new List<BuffStatusEffect>();
    private ShootingAddBuffSO DataReal { get { return Data as ShootingAddBuffSO; } }
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
	    foreach (var x in effects)
	    {
		    stats.RemoveEffect(x);
	    }
		PlayerEvent.OnShoot -= AddSpeed;
	}

    public override void HandleStackChange()
    {
        
    }
	public void AddSpeed()
	{
		dem++;

		if (dem == DataReal.BuffAfter)
		{
			dem = 0;
			BuffStatusEffect buff = DataReal.BuffSO1.AddStatusEffect(stats);
			effects.Add(buff);
			buff.OnEnd += () => { effects.Remove(buff); };
		}
	}
}