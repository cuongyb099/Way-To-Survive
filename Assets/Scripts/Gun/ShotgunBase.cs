using System.Collections;
using UnityEngine;

public class ShotgunBase : GunBase
{

    // Use this for initialization
    void Start()
    {
        GunRecoil = 1f;
    }

    public override void BulletInstantiate()
    {
        for (int i = 0; i < 5; i++)
        {
            base.BulletInstantiate();
        }
    }
    public override void GunRecoilUpdate()
    {
    }
    public override void ResetRecoil()
    {
    }
}