using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "new Stats",menuName ="BaseStats")]
public class BaseStatsData: ScriptableObject
{
    public float Health;
    public float Attack;
    public float Defence;
    public float Speed;
    public float AttackSpeedMutiplier;
    public float CritRate;
    public float CritDamage;
    public float DamageBonus;
}