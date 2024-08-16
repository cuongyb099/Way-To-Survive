using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageInfo
{
    public GameObject Dealer;
    public float Damage;
    public DamageInfo(GameObject dealer = null, float damage = 0)
    {
        Dealer = dealer;
        Damage = damage;
    }
}
