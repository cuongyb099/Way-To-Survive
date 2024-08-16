using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Damagable
{
    public interface IDamagable
    {
        public float HP { get; set; }
        public void Damage(DamageInfo info);
    }
}