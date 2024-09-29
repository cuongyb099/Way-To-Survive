using System;
using System.Collections;
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
        float finalDamage = Mathf.Clamp(info.Damage - Stats.GetStat(StatType.DEF).Value, 0, 999999);
        Stats.GetAttribute(AttributeType.Hp).Value -= finalDamage;
        OnDamaged?.Invoke();
        if (Stats.GetAttribute(AttributeType.Hp).Value <= 0)
        {
            Stats.GetAttribute(AttributeType.Hp).Value = 0;
            Death();
        }
    }

    public virtual void Death()
    {
        Debug.Log(gameObject.name + "has been slain!!");
        OnDeath?.Invoke();
    }
}
