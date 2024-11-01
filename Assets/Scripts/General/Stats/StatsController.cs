using System;
using System.Collections.Generic;
using System.Linq;
using Tech.Logger;
using UnityEngine;

public class StatsController : MonoBehaviour
{
	[SerializeField] private StatsHolderSO _statsHolder;

	protected Dictionary<StatType, Stat> _stats;
	protected Dictionary<AttributeType, Attribute> _attributes;
	protected List<BaseStatusEffect> _statusEffects = new List<BaseStatusEffect>();

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

	protected virtual void InitAttribute()
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

	public virtual void AddModifier(StatType type, StatModifier modifier)
	{
		if (_stats.TryGetValue(type, out Stat value))
		{
			value.AddModifier(modifier);
			return;
		}

		LogCommon.LogError($"{type} Not Found In {_statsHolder.name}");
	}

	public virtual void RemoveModifier(StatType type, StatModifier modifier)
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

	private void UpdateStatusEffect()
	{
		for (int i = _statusEffects.Count - 1; i >= 0; --i)
		{
			if (_statusEffects[i].ForceStop)
			{
				_statusEffects[i].Stop();
				_statusEffects.RemoveAt(i);
				OnChange?.Invoke();
				continue;
			}
			
			if (!_statusEffects[i].MonoUpdate()) continue;

			_statusEffects[i].Stop();
			_statusEffects.RemoveAt(i);
			OnChange?.Invoke();
		}
	}

	public void ApplyEffect(BaseStatusEffect effect)
	{
		if (!effect.Data.Stackable)
		{
			effect.Begin();
			_statusEffects.Add(effect);
			OnChange?.Invoke();
			return;
		}
		
		AddEffectStackable(effect);
	}

	public void RemoveEffect(BaseStatusEffect effect)
	{
		if (_statusEffects.Contains(effect))
		{
			effect.Stop();
			_statusEffects.Remove(effect);
			OnChange?.Invoke();
		}
	}

	private void AddEffectStackable(BaseStatusEffect effect)
	{
		if (_statusEffects.Contains(effect))
		{
			if (effect.CurrentStack >= effect.Data.MaxStack) return;
			effect.CurrentStack++;
			effect.HandleStackChange(StackStatus.Increase);
			OnChange?.Invoke();
			return;
		}
		
		effect.Begin();
		effect.CurrentStack++;
		_statusEffects.Add(effect);
		OnChange?.Invoke();
	}
	
	public Attribute GetAttribute(AttributeType type)
	{
		return _attributes.GetValueOrDefault(type);
	}

	public Stat GetStat(StatType type)
	{
		return _stats.GetValueOrDefault(type);
	}

	private void Update()
	{
		UpdateStatusEffect();
	}
}

