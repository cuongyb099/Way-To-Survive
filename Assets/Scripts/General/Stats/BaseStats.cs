
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public enum EStatType
{
	ATK,
    DEF,
    HP,
    SPD,
    ATKSPD,
    CRate,
    CDamage,
    DMGBonus,
}
public class BaseStats: MonoBehaviour
{
    public Dictionary<EStatType, Stat> StatsMap;
    [field: SerializeField] public Stat Health { get; private set; }
    [field: SerializeField] public Stat Attack { get; private set; }
    [field: SerializeField] public Stat Defence { get; private set; }
    [field: SerializeField] public Stat Speed { get; private set; }
    [field: SerializeField] public Stat AtkSpeed { get; private set; }
    [field: SerializeField] public Stat CritRate { get; private set; }
    [field: SerializeField] public Stat CritDMG { get; private set; }
    [field: SerializeField] public Stat DMGBonus { get; private set; }

    #region Unity Methods
    private void Update()
	{
        foreach (Stat s in StatsMap.Values)
        {
            s.UpdateModifierTimer();
        }
	}
	#endregion

	#region Public Methods
    public void LoadValues(BaseStatsData _data)
    {
        Health = new Stat(_data.Health);
        Attack = new Stat(_data.Attack);
        Defence = new Stat(_data.Defence);
        Speed = new Stat(_data.Speed);
        AtkSpeed = new Stat(_data.AttackSpeedMutiplier);
        CritRate = new Stat(_data.CritRate);
        CritDMG = new Stat(_data.CritDamage);
        DMGBonus = new Stat(_data.DamageBonus);
        SetStatsMap();
    }
    //public void LoadValues(float _health, float _attack, float _defend, float _speed, float _atkSpeed)
    //{
    //    StatsMap = new Dictionary<EStatType, Stat>();
    //    StatsMap.Add(EStatType.HP, new Stat(_health));
    //    StatsMap.Add(EStatType.ATK, new Stat(_attack));
    //    StatsMap.Add(EStatType.DEF, new Stat(_defend));
    //    StatsMap.Add(EStatType.SPD, new Stat(_speed));
    //    StatsMap.Add(EStatType.ATKSPD, new Stat(_atkSpeed));
    //    StatsMap.Add(EStatType.CRate, new Stat(_speed));
    //    StatsMap.Add(EStatType.CDamage, new Stat(_atkSpeed));
    //    StatsMap.Add(EStatType.DMGMul, new Stat(_atkSpeed));
    //}
    public void AddModifier(EStatType type, StatModifier statModifier)
    {
        StatsMap[type].AddModifier(statModifier);
    }
	public void AddModifier(EStatType[] types, StatModifier statModifier)
	{
        foreach(EStatType type in types)
        {
			StatsMap[type].AddModifier(statModifier);
        }
	}
	public void RemoveModifier(EStatType type, StatModifier statModifier)
	{
		StatsMap[type].RemoveModifier(statModifier);
	}
	public void RemoveModifier(EStatType[] types, StatModifier statModifier)
	{
		foreach (EStatType type in types)
		{
			StatsMap[type].RemoveModifier(statModifier);
		}
	}
	#endregion

	#region Private Methods
	private void SetStatsMap()
    {
        StatsMap = new Dictionary<EStatType, Stat>();
        StatsMap.Add(EStatType.HP, Health);
        StatsMap.Add(EStatType.ATK, Attack);
        StatsMap.Add(EStatType.DEF, Defence);
        StatsMap.Add(EStatType.SPD, Speed);
        StatsMap.Add(EStatType.ATKSPD, AtkSpeed);
        StatsMap.Add(EStatType.CRate, CritRate);
        StatsMap.Add(EStatType.CDamage, CritDMG);
        StatsMap.Add(EStatType.DMGBonus, DMGBonus);
    }
	#endregion

}
