using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum EModifierType
    {
        BaseFlat,
        Percentage,
        Flat,
    }
    public class StatModifier
    {
        public float Value { get; }
        public EModifierType ModifierType { get; }
        public float Timer { get; set; }

        public StatModifier(float _value, EModifierType _modifierType, float _timer = -1f)
        {
            Value = _value;
            ModifierType = _modifierType;
            Timer = _timer;
        }

        public StatModifier() : this(0f, EModifierType.Flat) { }
        public StatModifier(StatModifier modifier) : this(modifier.Value, modifier.ModifierType, modifier.Timer) { }
    }
