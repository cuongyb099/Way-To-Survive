using System;
using UnityEngine;

public abstract class BaseStat : MonoBehaviour, IDamagable
{
    [field:SerializeField] public float HP { get; protected set; }
	public Action OnDamaged { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public Action OnDeath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	public bool IsDamaged;

    public virtual bool IsDead() => HP <= 0;

    public virtual void Damage(DamageInfo info)
    {
            
    }

    public virtual void Damage(float Damage)
    {
        IsDamaged = true;
        HP -= Damage;
        if (HP < 0) HP = 0;
    }

	public void Death()
	{
		throw new NotImplementedException();
	}
}
