
using System.Collections.Generic;
using KatInventory;
using UnityEngine;

public class WeaponData : ItemGOData
{
    public WeaponType WeaponType{ get; private set; }
    public float ShootingSpeed{ get; private set; }
    public float Damage{ get; private set; }
    public float Weight{ get; private set; }
    public bool ReleaseToShoot{ get; private set; }
    public List<AudioClip> AttackSounds{ get; private set; }
    public WeaponData(WeaponBaseSO staticData, int quantity, GameObject go) : base(staticData, quantity, go)
    {
        WeaponType = staticData.WeaponType;
        ShootingSpeed = staticData.ShootingSpeed;
        Damage = staticData.Damage;
        Weight = staticData.Weight;
        ReleaseToShoot = staticData.ReleaseToShoot;
        AttackSounds = staticData.AttackSounds;
    }
}
