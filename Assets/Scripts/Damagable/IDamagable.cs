﻿using System;
using UnityEngine;

public interface IDamagable
{
    public bool IsDead { get;}
    public Action OnDamaged { get; set; }
    public Action OnDeath { get; set; }
    public void Damage(DamageInfo info);
    public void Death(GameObject dealer);
}