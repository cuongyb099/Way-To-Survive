using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffSO", menuName = "Item/Buff/new BuffSO")]
public class BuffSO : ScriptableObject
{
    public string BuffName;
	[TextArea]
	public string BuffDescription;
	public StatType StatType;
    public StatModType ModifierType;
	public float Value;
	public bool HasDuration;
	public float Duration;
}
