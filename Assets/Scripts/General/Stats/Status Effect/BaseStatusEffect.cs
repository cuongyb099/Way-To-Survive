///Package Create By Kat
using System;
using System.Threading.Tasks;
using UnityEngine;

public enum StackStatus
{
    Increase,
    Decrease,
}
/// <summary>
/// Not Stackable Is Clone Effect
/// Stackable Is Reference Effect 
/// </summary>
public abstract class BaseStatusEffect 
{
    public Action OnStart, OnEnd, OnActive;
    protected float timer;
    protected StatsController stats;
    public bool ForceStop { get; protected set; }
    public int CurrentStack;

    public StatusEffectSO Data;
    
    protected BaseStatusEffect(StatusEffectSO data, StatsController target,
        Action OnStart = null, Action onEnd = null, Action onActive = null)
    {
        OnInit(data, target);
        
        this.OnStart = OnStart;
        this.OnEnd = onEnd;
        this.OnActive = onActive;
    }
    
    protected BaseStatusEffect(){}

    

    /// <summary>
    /// This Function To Active Effect
    /// </summary>
    public void Begin()
    {
        ForceStop = false;
        CurrentStack = 0;
        HandleStart();
        OnStart?.Invoke();
        if (Data.UseAdvanceUpdate)
        {
            _ = UpdateAsync();
        }
    }
    
    protected virtual void OnInit(StatusEffectSO data, StatsController target)
    {
        ForceStop = false;
        timer = 0f;
        Data = data;
        stats = target;
        CurrentStack = 0;
    }
    
    /// <summary>
    /// This MonoUpdate Work With Effect Stack Same Timer  
    /// </summary>
    public virtual bool MonoUpdate()
    {
        if (!Data.UseAdvanceUpdate)
        {
            HandleOnUpdate();
            OnActive?.Invoke();
        }
        if (!Data.HasDuration) return false;
        timer += Time.deltaTime;
        return timer > Data.Duration;
    }

    private async Task UpdateAsync()
    {
        while (!ForceStop)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) break;
#endif
            HandleOnUpdate();
            OnActive?.Invoke();
            await Task.Yield();
        }
    }

    public void Stop()
    {
        ForceStop = true;
        HandleOnEnd();
        OnEnd?.Invoke();
    }

    
    /// <summary>
    /// Clone Must Override In Other Effect
    /// </summary>
    public virtual BaseStatusEffect Clone()
    {
        return (BaseStatusEffect)this.MemberwiseClone();
    }
    
    /// <summary>
    /// HandleStackChange Not Apply When Begin
    /// </summary>
    public abstract void HandleStackChange(StackStatus stackStatus);
    protected abstract void HandleStart();
    protected abstract void HandleOnUpdate();
    protected abstract void HandleOnEnd();

}

