using UnityEngine;

public abstract class BaseStat : MonoBehaviour, IDamagable
{
    [field:SerializeField] public float HP { get; protected set; }

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
}
