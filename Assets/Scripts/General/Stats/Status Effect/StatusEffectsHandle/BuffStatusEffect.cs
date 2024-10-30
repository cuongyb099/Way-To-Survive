using System;
using System.Collections;
using UnityEngine;

public class BuffStatusEffect : BaseStatusEffect
{
    public BuffStatusEffect(BasicBuffSO data, StatsController target, Action OnStart = null, Action onEnd = null, Action onActive = null) : base(data, target, OnStart, onEnd, onActive)
    {

    }

    protected override void HandleStart()
    {

    }
    protected override void HandleOnUpdate()
    {

    }
    protected override void HandleOnEnd()
    {

    }

    public override void HandleStackChange(StackStatus stackStatus)
    {
        
    }
}