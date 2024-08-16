using UnityEngine;

public class Stat : MonoBehaviour, IDamagable
{
    public float HP { get; set; }

    public virtual void Damage(DamageInfo info)
    {
        
    }
}
