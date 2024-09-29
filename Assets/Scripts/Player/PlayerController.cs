using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UIElements;

public class PlayerController : BasicController
{
    public Rigidbody Rigidbody { get; private set; }
    public CapsuleCollider Collider { get; private set; }
    public Animator Animator { get; private set; }

	//Player Data
	public float GunSwitchCooldown = .1f;
    public GunBase[] StartGun;
    public LayerMask GroundLayer;
    //GunSystem
    public Transform GunHoldPoint;
    public LineRendererHelper LineRendererL;
    public LineRendererHelper LineRendererR;
    public List<GunBase> Guns { get; private set; }
    public int CurrentGunIndex { get; private set; }

    private bool gunSwitchable = true;

    protected override void Awake()
    {
        base.Awake();
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<CapsuleCollider>();
        Animator = GetComponentInChildren<Animator>();
        Guns = new List<GunBase>();
		for (int i = 0; i < StartGun.Length; i++)
		{
			Guns.Add(Instantiate(StartGun[i], GunHoldPoint.transform));
			Guns[i].gameObject.layer = this.gameObject.layer;
			Guns[i].gameObject.SetActive(false);
			Guns[i].OnShoot += AnimShoot;
		}
		PlayerInput.Instance.OnSwitchGuns += SwitchGun;
	}
    private void OnDestroy()
    {
        if (Stats != null)
        {
            _hp.OnValueChange -= HandleHealthChange;
            _maxHp.OnValueChange -= HandleMaxHpChange;
        }

        if (PlayerInput.Instance != null) 
            PlayerInput.Instance.OnSwitchGuns -= SwitchGun;
    }
    private void Start()
    {
		EquipGun(0);
        InitHealthBar();
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
        Debug.Log((int)Guns[CurrentGunIndex].GunData.WeaponType);
    }
	private void Update()
	{
		SetLineRenderers();
	}
	// Gun handle
	public void EquipGun(int index)
    {
        Guns[CurrentGunIndex].gameObject.SetActive(false);
        Guns[index].gameObject.SetActive(true);
        CurrentGunIndex = index;
        Animator.SetFloat("WeaponType", (float)Guns[index].GunData.WeaponType);
        
    }
    [ContextMenu("sw")]
    public void SwitchGun()
    {
        if(!gunSwitchable) return;
        gunSwitchable = false;
        DOVirtual.DelayedCall(GunSwitchCooldown, () => { gunSwitchable = true; });
        //Switch
		DisableShooting();
		Guns[CurrentGunIndex].ResetRecoil();
        EquipGun((CurrentGunIndex+1)%(Guns.Count));
    }
	public void AnimShoot()
	{
		Animator.SetTrigger("Shoot");
	}
	public void DisableShooting()
	{
		foreach (var gun in Guns)
		{
			gun.ShootAble = false;
		}
		Animator.SetBool("SwitchWeapon", true);
        DisableLineRenderer();
	}
	public void EnableShooting()
	{
		EnableLineRenderer();
		Animator.SetBool("SwitchWeapon", false);
		foreach (var gun in Guns)
		{
			gun.ShootAble = true;
		}
	}
	public void EnableLineRenderer()
	{
		LineRendererL.LR.enabled = true;
		LineRendererR.LR.enabled = true;
	}
	public void DisableLineRenderer()
    {
        LineRendererL.LR.enabled = false;
		LineRendererR.LR.enabled = false;
	}
	public void SetLineRenderers()
	{
        GunBase gun = Guns[CurrentGunIndex];
        float accuracy = gun.GunData.SpreadMax * gun.GunRecoil;
		LineRendererL.SetLineRenderer(gun.ShootPoint, gun.GunData.Aim, Quaternion.Euler(0, -accuracy, 0) * transform.forward);
		LineRendererR.SetLineRenderer(gun.ShootPoint, gun.GunData.Aim, Quaternion.Euler(0, accuracy, 0) * transform.forward);
	}

	// Movement
	Vector2 CurrentBlend;
    private void MovePlayer()
    {
        Vector3 MovementInput = PlayerInput.Instance.MovementInput;
        Rigidbody.velocity = new Vector3(0f, Rigidbody.velocity.y, 0f);
        Rigidbody.AddForce(MovementInput * Stats.GetStat(StatType.Speed).Value, ForceMode.VelocityChange);
        //Animation
        var x =Vector3.Dot(MovementInput, transform.right);
        var y =Vector3.Dot(MovementInput, transform.forward);
        CurrentBlend = Vector2.Lerp(CurrentBlend, new Vector2(x,y) , 0.1f);
        Animator.SetFloat("PosX",CurrentBlend.x);
        Animator.SetFloat("PosY",CurrentBlend.y);
    }

    public void RotatePlayer()
    {
        Vector2 rotateInput = PlayerInput.Instance.RotationInput;
        if (rotateInput == Vector2.zero) return;
        float atan = Mathf.Atan2(rotateInput.x,rotateInput.y)*Mathf.Rad2Deg;
        Rigidbody.rotation = Quaternion.Euler(0,atan,0);
    }


    public override void Death()
    {
        base.Death();
        Guns[CurrentGunIndex].gameObject.SetActive(false);
    }
    
	//Health Bar
	private Attribute _hp;
    private Stat _maxHp;
    private void InitHealthBar()
    {
        if (Stats.TryGetAttribute(AttributeType.Hp, out _hp))
        {
            _hp.OnValueChange += HandleHealthChange;
            PlayerEvent.OnInitStatusBar?.Invoke(AttributeType.Hp, _hp.Value, _hp.MaxValue);
        }

        if (Stats.TryGetStat(StatType.MaxHP, out _maxHp))
        {
            _maxHp.OnValueChange += HandleMaxHpChange;
        }
    }

    private void HandleMaxHpChange()
    {
        PlayerEvent.OnMaxHeathChange?.Invoke(_hp.Value, _maxHp.Value);
    }

    private void HandleHealthChange()
    {
        if (_hp == null) return;
        PlayerEvent.OnHeathChange?.Invoke(_hp.Value, _hp.MaxValue);
    }
}
