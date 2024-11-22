using System;
using System.Collections.Generic;

[Serializable]
public class Stat
{
	private float _baseValue;

	public float BaseValue
	{
		get { return _baseValue; }
		set { _baseValue = value; _isDirty = true; }
	}

	private bool _isDirty = true;

	private float _value;

	public virtual float Value
	{
		get
		{
			if (_isDirty)
			{
				_value = CalculateFinalValue();
				_isDirty = false;
			}
			return _value;
		}
	}

	public Action OnValueChange;

	protected readonly List<StatModifier> statModifiers;
	public List<StatModifier> StatModifiers => statModifiers;

	public Stat()
	{
		statModifiers = new List<StatModifier>();
	}

	public Stat(float baseValue) : this()
	{
		_baseValue = baseValue;
	}

	public virtual void AddModifier(StatModifier mod)
	{
		_isDirty = true;
		statModifiers.Add(mod);
		OnValueChange?.Invoke();
	}

	public virtual bool RemoveModifier(StatModifier mod)
	{
		if (statModifiers.Remove(mod))
		{
			_isDirty = true;
			OnValueChange?.Invoke();
			return true;
		}
		return false;
	}

	public virtual bool RemoveAllModifiersFromSource(object source)
	{
		int numRemovals = statModifiers.RemoveAll(mod => mod.Source == source);

		if (numRemovals > 0)
		{
			_isDirty = true;
			OnValueChange?.Invoke();
			return true;
		}
		return false;
	}

	protected virtual float CalculateFinalValue()
	{
		float finalValue = _baseValue;
		float sumPercentAdd = 0;

		for (int i = 0; i < statModifiers.Count; i++)
		{
			StatModifier mod = statModifiers[i];

			if (mod.Type == StatModType.Flat)
			{
				finalValue += mod.Value;
			}
			else if (mod.Type == StatModType.PercentAdd)
			{
				sumPercentAdd += mod.Value;

				if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
				{
					finalValue *= 1 + sumPercentAdd/100;
					sumPercentAdd = 0;
				}
			}
			else if (mod.Type == StatModType.PercentMult)
			{
				finalValue *= 1 + mod.Value;
			}
		}


		return (float)Math.Round(finalValue, 4);
	}
}
