using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageInfo
{
    public GameObject Dealer;
    public float Damage;
	public bool IsCrit;

    public DamageInfo(GameObject dealer = null, float damage = 0, bool isCrit = false)
    {
        Dealer = dealer;
        Damage = damage;
        IsCrit = isCrit;
    }
}
