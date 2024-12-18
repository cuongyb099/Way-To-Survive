using DG.Tweening;
using ResilientCore;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BasicController
{
    public Rigidbody Rigidbody { get; private set; }
    public FloatingCapsule FloatingCapsule { get; private set; }
    public CapsuleCollider Collider { get; private set; }
    public Animator Animator { get; private set; }
	//Player Data
	public float GunSwitchCooldown = .1f;
    public LayerMask GroundLayer;
    public GunBase StartingGun;
    public List<GunBase> OwnedGuns { get; private set; }
    public int Cash
    {
	    get => cash;
	    set
	    {
		    cash = value;
		    if (cash < 0)
		    {
			    cash = 0;
		    }
		    PlayerEvent.OnCashChange?.Invoke(cash);
	    }
    }
    private int cash;
    //GunSystem
    public Transform GunHoldPoint;
    public LineRendererHelper LineRendererL;
    public LineRendererHelper LineRendererR;
    public GunBase[] Guns;
    public int CurrentGunIndex { get; private set; }

    private bool gunSwitchable = true;

    protected override void Awake()
    {
        base.Awake();
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<CapsuleCollider>();
        FloatingCapsule = GetComponent<FloatingCapsule>();
        Animator = GetComponentInChildren<Animator>();
        
        BuffList = new List<int>();
        OwnedGuns = new List<GunBase> { StartingGun };

        Guns = new GunBase[3];
		InstantiateGun(StartingGun, 0);
			
		//InputEvent.OnInputSwitchGuns += SwitchGun;
        InputEvent.OnInputReloadGun += ReloadGun;
		PlayerEvent.OnShoot += SetShootAnim;
        Stats.GetStat(StatType.MagCapacity).OnValueChange += CalculateMaxCap;
		Stats.GetStat(StatType.ShootSpeed).OnValueChange += SetShootingSpeedAnim;
		Stats.GetStat(StatType.Speed).OnValueChange += SetMovementSpeedAnim;
	}
    private void OnDestroy()
    {
        _hp.OnValueChange -= HandleHealthChange;
        _maxHp.OnValueChange -= HandleMaxHpChange;
		//InputEvent.OnInputSwitchGuns -= SwitchGun;
		InputEvent.OnInputReloadGun -= ReloadGun;
		PlayerEvent.OnShoot -= SetShootAnim;
		Stats.GetStat(StatType.MagCapacity).OnValueChange -= CalculateMaxCap;
		Stats.GetStat(StatType.ShootSpeed).OnValueChange -= SetShootingSpeedAnim;
		Stats.GetStat(StatType.Speed).OnValueChange -= SetMovementSpeedAnim;
	}
	private void Start()
    {
        CalculateMaxCap();
		EquipGun(0);
        InitHealthBar();
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
        Float();
    }
	private void Update()
	{
		SetLineRenderers();
    }

    public override void Death(GameObject dealer)
    {
        base.Death(dealer);
        Guns[CurrentGunIndex].gameObject.SetActive(false);
    }

    // Gun handle
    public void InstantiateGun(GunBase gun, int index)
    {
	    if (Guns[index] != null)
	    {
		    Destroy(Guns[index].gameObject);
	    }
	    Guns[index] = (Instantiate(gun, GunHoldPoint.transform));
	    Guns[index].gameObject.layer = gameObject.layer;
	    Guns[index].Initialize();
	    Guns[index].gameObject.SetActive(false);
    }

    public void SetPlayerGuns(GunBase[] guns)
    {
	    for (int i = 0; i < Guns.Length; i++)
	    {
		    InstantiateGun(guns[i],i);
	    }
    }
    public bool EquipGun(int index)
    {
	    GunBase currentSlot = Guns[index];

	    if (currentSlot == null) return false;
        Guns[CurrentGunIndex].gameObject.SetActive(false);
        currentSlot.gameObject.SetActive(true);
        CurrentGunIndex = index;
        Animator.SetFloat("WeaponType", (float)currentSlot.GunData.WeaponType);
        Stats.GetStat(StatType.Speed).AddModifier(new StatModifier(-currentSlot.GunData.Weight,StatModType.Flat));
        PlayerEvent.OnSwitchGuns?.Invoke(currentSlot);
        return true;
    }

    public void SwitchGun(int index)
    {
	    if(!gunSwitchable ||
	       CurrentGunIndex == index ||
	       Guns[index]== null) return;
	    gunSwitchable = false;
	    DOVirtual.DelayedCall(GunSwitchCooldown, () => { gunSwitchable = true; });
	    //Switch
	    DisableBeforeSwitching();
	    Animator.SetBool("SwitchWeapon", true);
	    Animator.SetBool("ReloadGun", false);
	    Guns[CurrentGunIndex].ResetRecoil();
	    Stats.GetStat(StatType.Speed).RemoveModifier(new StatModifier(-Guns[CurrentGunIndex].GunData.Weight,StatModType.Flat));
	    EquipGun(index);
    }
    public void SwitchGun()
    {
        if(!gunSwitchable ) return;
        gunSwitchable = false;
        DOVirtual.DelayedCall(GunSwitchCooldown, () => { gunSwitchable = true; });
        //Switch
		DisableBeforeSwitching();
		Animator.SetBool("SwitchWeapon", true);
        Animator.SetBool("ReloadGun", false);
        Guns[CurrentGunIndex].ResetRecoil();
        Stats.GetStat(StatType.Speed).RemoveModifier(new StatModifier(-Guns[CurrentGunIndex].GunData.Weight,StatModType.Flat));
        int n = 1;
        while (n < Guns.Length)
        {
	        if(EquipGun((CurrentGunIndex+1)%Guns.Length)) break;
	        n++;
        }
    }
    bool gunReloadable = true;
    public void ReloadGun()
    {
		if (!gunReloadable || Guns[CurrentGunIndex].IsFullCap) return;
		DisableBeforeSwitching();
		Animator.SetBool("ReloadGun", true);
		Guns[CurrentGunIndex].ResetRecoil();
	}

	public void DisableBeforeSwitching()
	{
		foreach (var gun in Guns)
		{
			if(gun == null) continue;
			gun.ShootAble = false;
		}
        gunReloadable = false;
        DisableLineRenderer();
	}
	public void EnableAfterSwitching()
	{
		EnableLineRenderer();
        gunReloadable = true;
		foreach (var gun in Guns)
		{
			if(gun == null) continue;
			gun.ShootAble = true;
		}
	}
    public void AfterReload()
    {
        Guns[CurrentGunIndex].Stats.GetAttribute(AttributeType.Bullets).SetValueToMax();
		PlayerEvent.OnReload?.Invoke();
		Animator.SetBool("ReloadGun", false);
        EnableAfterSwitching();
	}
    public void SetShootingSpeedAnim()
    {
		Animator.SetFloat("ShootingSpeed",Stats.GetStat(StatType.ShootSpeed).Value);
	}
	public void SetMovementSpeedAnim()
	{
		Animator.SetFloat("MovementSpeed", Stats.GetStat(StatType.Speed).Value/5f);
	}
	public void SetShootAnim()
	{
		Animator.SetTrigger("Shoot");
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
		LineRendererL.SetLineRenderer(gun.ShootPoint, gun.GunData.Aim, Quaternion.Euler(0, Mathf.Clamp(-accuracy, -15,0), 0) * transform.forward);
		LineRendererR.SetLineRenderer(gun.ShootPoint, gun.GunData.Aim, Quaternion.Euler(0, Mathf.Clamp(accuracy, 0, 15), 0) * transform.forward);
	}

	// Movement
	Vector2 CurrentBlend;
    private void MovePlayer()
    {
        Vector3 MovementInput = PlayerInput.Instance.MovementInput;
        Rigidbody.velocity = new Vector3(0f, Rigidbody.velocity.y, 0f);
        Rigidbody.AddForce(MovementInput * (Stats.GetStat(StatType.Speed).Value), ForceMode.VelocityChange);
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
    public void Float()
    {
        Ray ray = new Ray(FloatingCapsule.CapsuleColliderData.Collider.bounds.center, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, FloatingCapsule.FloatingData.FloatRayLength, GroundLayer, QueryTriggerInteraction.Ignore))
        {
            float distanceFromGround = FloatingCapsule.CapsuleColliderData.ColliderCenterLocalSpace.y * transform.localScale.y - hit.distance;
            if (distanceFromGround == 0)
            {
                return;
            }
            float liftAmount = distanceFromGround * FloatingCapsule.FloatingData.StepHeightMultiplier - Rigidbody.velocity.y;
            Rigidbody.AddForce(new Vector3(0, liftAmount, 0), ForceMode.VelocityChange);
            
            return;
        }
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
    //Buffs
    public List<int> BuffList { get; private set; }
    public void AddBuffToPlayer(BasicBuffSO buff)
    {
		BuffStatusEffect buffEffect = buff.AddStatusEffect(Stats);
        BuffList.Add(buff.ID);
        buffEffect.OnEnd += () => BuffList.Remove(buff.ID);
    }

    public void CalculateMaxCap()
    {
	    for (int i = 0; i < Guns.Length; i++)
	    {
		    if(Guns[i] == null) continue;
		    Guns[i].SetBulletCap(Stats.GetStat(StatType.MagCapacity).Value);
	    }

	    PlayerEvent.OnChangeCap?.Invoke();
    }
}
