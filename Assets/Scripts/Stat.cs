using UnityEngine;

public class Stat : MonoBehaviour, IDamagable
{
    public float Hp { get; set; }

    public virtual void Damage(DamageInfo info)
    {
        
    }
}
