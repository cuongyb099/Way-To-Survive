using System;
using System.Collections;
using Tech.Logger;
using TMPro.EditorUtilities;
using UnityEngine;

public class BasicController : MonoBehaviour, IDamagable
{
    public StatsController Stats { get; private set; }
    public Action OnDamaged { get; set; }
    public Action OnDeath { get; set; }

    protected virtual void Awake()
    {
        Stats = GetComponentInChildren<StatsController>();
    }
    public virtual void Damage(DamageInfo info)
    {
        DamageFormular.Damage(info, Stats);
        OnDamaged?.Invoke();
        
        var hp = Stats.GetAttribute(AttributeType.Hp);
        
        if (hp.Value <= 0)
        {
            hp.Value = 0;
            Death();
        }
    }

    public virtual void Death()
    {
        LogCommon.Log($"{gameObject.name} has been slain!!");
        OnDeath?.Invoke();
    }
}
