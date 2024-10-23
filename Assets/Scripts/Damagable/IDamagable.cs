using System;

public interface IDamagable
{
    public Action OnDamaged { get; set; }
    public Action OnDeath { get; set; }
    public void Damage(DamageInfo info);
    public void Death();
}