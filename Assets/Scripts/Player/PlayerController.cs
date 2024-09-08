using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class PlayerController : MonoBehaviour, IDamagable
{
    public Rigidbody Rigidbody { get; private set; }
    public CapsuleCollider Collider { get; private set; }
    public Animator Animator { get; private set; }
    public BaseStats BaseStats { get; private set; }
    public float HP { get; set; }
    //Player Data
    public float Speed = 5.0f;
    public float GunSwitchCooldown = .1f;
    public BaseStatsData StatData;
    public GunBase[] StartGun;
    public LayerMask GroundLayer;
    //GunSystem
    public Transform GunHoldPoint;
    public List<GunBase> Guns { get; private set; }
    public int CurrentGunIndex { get; private set; }
    public Action OnDamaged { get; set; }

    private bool gunSwitchable = true;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        BaseStats = GetComponent<BaseStats>();
        Collider = GetComponent<CapsuleCollider>();
        Guns = new List<GunBase>();
        for (int i = 0; i < StartGun.Length; i++)
        {
            Guns.Add(Instantiate(StartGun[i], GunHoldPoint.transform));
            Guns[i].gameObject.layer = this.gameObject.layer;
            Guns[i].gameObject.SetActive(false);
        }

        PlayerInput.Instance.OnSwitchGuns += SwitchGun;
    }
    private void OnDestroy()
    {
        if (PlayerInput.Instance == null) return;
        PlayerInput.Instance.OnSwitchGuns -= SwitchGun;
    }
    private void Start()
    {
        BaseStats.LoadValues(StatData);
        HP = BaseStats.StatsMap[EStatType.HP].Value;
        EquipGun(0);
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    public void EquipGun(int index)
    {
        Guns[CurrentGunIndex].gameObject.SetActive(false);
        Guns[index].gameObject.SetActive(true);
        CurrentGunIndex = index;
    }
    [ContextMenu("sw")]
    public void SwitchGun()
    {
        if(!gunSwitchable) return;
        gunSwitchable = false;
        DOVirtual.DelayedCall(GunSwitchCooldown, () => { gunSwitchable = true; });
        //Switch
        Guns[CurrentGunIndex].ResetRecoil();
        EquipGun((CurrentGunIndex+1)%(Guns.Count));
    }
    private void MovePlayer()
    {
        Rigidbody.velocity = new Vector3(0f, Rigidbody.velocity.y, 0f);
        Rigidbody.AddForce(PlayerInput.Instance.MovementInput * Speed, ForceMode.VelocityChange);
    }

    public void RotatePlayer()
    {
        Vector2 rotateInput = PlayerInput.Instance.RotationInput;
        if (rotateInput == Vector2.zero) return;
        float atan = Mathf.Atan2(rotateInput.x,rotateInput.y)*Mathf.Rad2Deg;
        Rigidbody.rotation = Quaternion.Euler(0,atan,0);
    }

    public void Damage(DamageInfo info)
    {
        HP -= info.Damage;
        if (HP <=0)
        {
            HP = 0;
            Death();
        }
    }
    public void Death()
    {
        Guns[CurrentGunIndex].gameObject.SetActive(false);
    }
}
