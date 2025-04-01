
using DG.Tweening;
using UnityEngine;

public class MeleeBase : WeaponBase
{
    //fix
    //public GunData MeleeData => (GunData)WeaponData;
    private BoxCollider hitBox;
    private int comboCount = 0;
    
    private Collider[] hitColliders = new Collider[30];
    private void Start()
    {
        hitBox = playerController.MeleeHitCollider;
    }

    public override void Shoot()
    {
        if (!ShootAble ||
            !repeatAble ) return;
        repeatAble = false;
        PlayerEvent.OnAttack?.Invoke();
        playerController.Animator.SetFloat("MeleeCombo", ++comboCount%2);
        DOVirtual.DelayedCall(WeaponData.ShootingSpeed/playerController.Stats.GetStat(StatType.ATKSpeed).Value, () => { repeatAble = true;});
        WeaponSoundPlay();
    }

    public void DealDamage()
    {
        Physics.OverlapBoxNonAlloc(hitBox.bounds.center, hitBox.bounds.extents, hitColliders,Quaternion.identity);
        //hitColliders = Physics.OverlapBox(hitBox.bounds.center, hitBox.bounds.extents,Quaternion.identity);
        foreach (var x in hitColliders)
        {
            if(!x.TryGetComponent<IDamagable>(out IDamagable damagable)) return;
            if(playerController.CompareTag(x.gameObject.tag))return;
            DamageHandler.Damage(damagable,DamageInfo.GetDamageInfo(WeaponData.Damage,playerController.Stats, DamageType.Melee));
        }
    }
}
