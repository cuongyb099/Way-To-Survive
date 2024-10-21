using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffRarity
{
	Common,
	Uncommon,
	Rare,
	ExtremelyRare,
	Myth,
}
[CreateAssetMenu(fileName = "BuffSO", menuName = "Item/Buff/new BuffSO")]
public class BuffSO : ScriptableObject
{
    public string BuffName;
	[TextArea]
	public string BuffDescription;
	public Sprite BuffIcon;
	public BuffRarity RareType;
	public StatType StatType;
    public StatModType ModifierType;
	public float Value;
	public bool HasDuration;
	public float Duration;
}
