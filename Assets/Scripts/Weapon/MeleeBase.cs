using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MeleeBase : WeaponBase
{
    private int comboCount = 0;
    public override void Shoot()
    {
        if (!ShootAble ||
            !repeatAble ) return;
        repeatAble = false;
        PlayerEvent.OnShoot?.Invoke();
        playerController.Animator.SetFloat("MeleeCombo", ++comboCount%2);
        DOVirtual.DelayedCall(GunData.ShootingSpeed/playerController.Stats.GetStat(StatType.ShootSpeed).Value, () => { repeatAble = true;});
        WeaponSoundPlay();
    }
}
