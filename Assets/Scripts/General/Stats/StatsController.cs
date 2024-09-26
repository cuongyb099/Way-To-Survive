using System;
using System.Collections.Generic;
using Tech.Logger;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StatsController : MonoBehaviour
{
	[SerializeField] private StatsHolderSO _statsHolder;

	private Dictionary<StatType, Stat> _stats;
	private Dictionary<AttributeType, Attribute> _attributes;
	private List<BaseStatusEffect> _statusEffects = new List<BaseStatusEffect>();

	public Dictionary<StatType, Stat> Stats
	{
		get
		{
			InitStats();
			return _stats;
		}
	}

	public Dictionary<AttributeType, Attribute> Attributes
	{
		get
		{
			InitAttribute();
			return _attributes;
		}
	}

	public List<BaseStatusEffect> StatusEffects => _statusEffects;

	public Action OnChange;

	protected virtual void Awake()
	{
		InitStats();
		InitAttribute();
	}

	private void Update()
	{
		UpdateStatusEffect();
		if (Input.GetKeyDown(KeyCode.V))
		{
			MinusAttributeValue(AttributeType.Hp, 10);
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			MinusAttributeValue(AttributeType.Hp, -10);
		}
	}

	private void InitAttribute()
	{
		if (!_statsHolder || _attributes != null)
		{
			return;
		}

		_attributes = new Dictionary<AttributeType, Attribute>();
		foreach (AttributeType key in _statsHolder.AttributeItems.Keys)
		{
			AttributeItem attributeItem = _statsHolder.GetAttribute(key);

			Attribute attribute = new(attributeItem.MinValue, _stats[attributeItem.MaxValue], attributeItem.StartPercent, this);
			attribute.OnValueChange += () => OnChange?.Invoke();
			_attributes.Add(key, attribute);
		}
	}

	protected virtual void InitStats()
	{
		if (!_statsHolder || _stats != null)
		{
			return;
		}

		_stats = new Dictionary<StatType, Stat>();
		foreach (StatType key in _statsHolder.StatItems.Keys)
		{
			Stat stat = new(_statsHolder.StatItems[key].BaseValue);
			stat.OnValueChange += () => OnChange?.Invoke();
			_stats.Add(key, stat);
		}
	}

	public void AddModifier(StatType type, StatModifier modifier)
	{
		if (_stats.TryGetValue(type, out Stat value))
		{
			value.AddModifier(modifier);
			return;
		}

		LogCommon.LogError($"{type} Not Found In {_statsHolder.name}");
	}

	public void RemoveModifier(StatType type, StatModifier modifier)
	{
		_stats[type].RemoveModifier(modifier);
	}

	public void MinusAttributeValue(AttributeType type, float value)
	{
		if (TryGetAttribute(type, out Attribute attribute))
		{
			attribute.Value -= value;
		}
	}

	public bool TryGetAttribute(AttributeType type, out Attribute attribute)
	{
		if (_attributes.TryGetValue(type, out var value))
		{
			attribute = value;
			return true;
		}

		attribute = null;
		return false;
	}

	public bool TryGetStat(StatType type, out Stat stat)
	{
		if (_stats.TryGetValue(type, out var value))
		{
			stat = value;
			return true;
		}

		stat = null;
		return false;
	}
    public Attribute GetAttribute(AttributeType type)
    {
        if (_attributes.TryGetValue(type, out var value))
        {
            return value;
        }
        return null;
    }
    public Stat GetStat(StatType type)
	{
        if (_stats.TryGetValue(type, out var value))
        {
            return value;
        }
        return null;
    }

	private void UpdateStatusEffect()
	{
		for (int i = _statusEffects.Count - 1; i >= 0; --i)
		{
			if (!_statusEffects[i].Update()) continue;

			_statusEffects[i].Stop();
			_statusEffects.RemoveAt(i);
			OnChange?.Invoke();
		}
	}

	public void ApplyEffect(BaseStatusEffect effect)
	{
		_statusEffects.Add(effect);
	}


	[ContextMenu("Poison Effect")]
	public void test()
	{
		var poison = new PoisonEffect(true, this, 5f);
		_statusEffects.Add(poison);
	}

	[ContextMenu("Add 10 MaxHP")]
	public void test2()
	{
		AddModifier(StatType.MaxHP, new StatModifier(10, StatModType.Flat));
	}
}

