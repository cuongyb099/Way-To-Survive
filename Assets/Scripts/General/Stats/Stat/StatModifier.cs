using System;

public enum StatModType
{
	BaseFlat,
	Flat,
	Percentage
}

public class StatModifier: IEquatable<StatModifier>
{
	public float Value;
	public StatModType Type;
	public object Source;

	public StatModifier(float value, StatModType type, object source = null)
	{
		Value = value;
		Type = type;
		Source = source;
	}

	public StatModifier Clone()
	{
		return new StatModifier(Value, Type, Source);
	}

    public bool Equals(StatModifier other)
    {
        return (other.Value == Value && other.Type == Type && other.Source == Source);
    }
}

