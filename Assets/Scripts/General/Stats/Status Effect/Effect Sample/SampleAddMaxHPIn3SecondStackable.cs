#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sample For Stackable Effect
/// </summary>

public class SampleAddMaxHPIn3SecondStackable : BaseStatusEffect
{
    protected Stack<StatModifier> HpToAdd = new ();
    protected StatModifier BaseModifier;
    protected List<float> ListTimer = new ();
    
    public SampleAddMaxHPIn3SecondStackable(StatusEffectSO data, StatsController target, StatModifier hpToAdd,
        Action OnStart = null, Action onEnd = null, Action onActive = null) : base(data, target,OnStart, onEnd, onActive)
    {
        BaseModifier = hpToAdd;
        BaseModifier.Value = Mathf.Abs(hpToAdd.Value);
    }

    protected override void HandleStart()
    {
        var clone = BaseModifier.Clone();
        stats.AddModifier(StatType.MaxHP, clone);
        HpToAdd.Push(clone);
        ListTimer.Add(0);
    }

    public override bool MonoUpdate()
    {
        for (int i = ListTimer.Count - 1; i >= 0 ; i--)
        {
            ListTimer[i] += Time.deltaTime;
            if (ListTimer[i] > Data.Duration)
            {
                CurrentStack--;
                HandleStackChange(StackStatus.Decrease);
                ListTimer.RemoveAt(i);
            }
        }

        return ListTimer.Count <= 0;
    }

    public override void HandleStackChange(StackStatus stackStatus)
    {
        switch (stackStatus)
        {
            case StackStatus.Increase:
                var clone = BaseModifier.Clone();
                HpToAdd.Push(clone);
                stats.AddModifier(StatType.MaxHP, clone);
                ListTimer.Add(0);
                return;
            case StackStatus.Decrease:
                if(HpToAdd.Count < 0) return;
                stats.RemoveModifier(StatType.MaxHP, HpToAdd.Pop());
                return;
        }
    }

    protected override void HandleOnUpdate()
    {
        
    }

    protected override void HandleOnEnd()
    {
        while (HpToAdd.Count > 0)
        {
            stats.RemoveModifier(StatType.MaxHP, HpToAdd.Pop());
        }
    }
    
    public override BaseStatusEffect Clone()
    {
        return (SampleAddMaxHPIn3SecondStackable)this.MemberwiseClone();
    }
}
#endif
