using System;

public interface IDamagable
{
    public float HP { get; set; }
    public Action OnDamaged { get; set; }
    public void Damage(DamageInfo info)
    {
        HP -= info.Damage;
        if (HP <= 0)
        {
            HP = 0;
            Death();
        }
    }
    public void Death();
}