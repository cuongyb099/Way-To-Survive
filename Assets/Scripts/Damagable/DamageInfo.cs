using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageInfo
{
    public GameObject Dealer;
    public float Damage;
	public bool IsCrit;

    public DamageInfo(GameObject dealer = null, float damage = 0, bool isCrit = false)
    {
        Dealer = dealer;
        Damage = damage;
        IsCrit = isCrit;
    }

    public static DamageInfo GetDamageInfo(float multiplier, StatsController statsController, GameObject dealer = null)
    {
        if (!statsController.Stats.TryGetValue(StatType.ATK, out Stat atkStat)) 
            return new DamageInfo();
        if (!statsController.Stats.TryGetValue(StatType.CritRate, out Stat critRate) ||
            !statsController.Stats.TryGetValue(StatType.CritDamage, out Stat critDmg)) 
            return new DamageInfo(dealer, atkStat.Value * multiplier);
        
        bool doesCrit = UnityEngine.Random.value < critRate.Value;
        float finalDamage = atkStat.Value * multiplier * (1f + (doesCrit ? critDmg.Value : 0f));
        return new DamageInfo(dealer, finalDamage, doesCrit);
    }}
