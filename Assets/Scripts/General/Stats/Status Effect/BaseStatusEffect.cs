using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BaseStatusEffect
{
    public Action OnStart, OnEnd, OnActive;

	protected float duration;
	protected StatusEffectType type;
	protected float startTime;
    protected bool isCancelled;
    protected StatsController stats;
    protected bool hasDuration;

    public StatusEffectType Type => type;

    public BaseStatusEffect(bool hasDuration, StatsController target, float duration = 0f)
    {
        this.duration = duration;
        this.hasDuration = hasDuration;
        isCancelled = false;
        startTime = Time.time;
        stats = target;

        HandleStart();
        _ = UpdateRuntime();
    }

    public virtual bool Update()
    {
        if (!hasDuration) return false;
        return Time.time > startTime + duration;
    }

    protected virtual void HandleStart()
    {
		OnStart?.Invoke();
	}

	protected virtual async Task UpdateRuntime()
    {
        while (!isCancelled)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) break;
#endif
            
            HandleWhileActive();
            await Task.Yield();
        }
    }

    protected virtual void HandleWhileActive()
    {
        OnActive?.Invoke();
    }
    
    protected virtual void HandleEnd()
    {
        OnEnd?.Invoke();
    }

    public virtual void Stop()
    {
        isCancelled = true;
        HandleEnd();
    }
}
