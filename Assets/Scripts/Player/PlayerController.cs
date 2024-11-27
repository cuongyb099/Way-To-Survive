using DG.Tweening;
using ResilientCore;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : BasicController
{
    public Rigidbody Rigidbody { get; private set; }
    public FloatingCapsule FloatingCapsule { get; private set; }
    public CapsuleCollider Collider { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInteractor PlayerInteractor { get; private set; }
	//Player Data
	public float GunSwitchCooldown = .1f;
    public LayerMask GroundLayer;
    public WeaponBase StartingWeapon;
    public int StartingCash = 0;
    public List<WeaponBase> OwnedWeapons { get; private set; }
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
    //WeaponSystem
    public Transform WeaponHoldPoint;
    public LineRendererHelper LineRendererL;
    public LineRendererHelper LineRendererR;
    public WeaponBase[] Weapons;
    public WeaponBase CurrentWeapon => Weapons[CurrentWeaponIndex];
    public int CurrentWeaponIndex { get; private set; }

    private bool weaponSwitchable = true;

    protected override void Awake()
    {
        base.Awake();
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<CapsuleCollider>();
        FloatingCapsule = GetComponent<FloatingCapsule>();
        Animator = GetComponentInChildren<Animator>();
        PlayerInteractor = GetComponentInChildren<PlayerInteractor>();
        
        BuffList = new List<int>();
        OwnedWeapons = new List<WeaponBase> { StartingWeapon };

        Weapons = new WeaponBase[3];
		InstantiateGun(StartingWeapon, 0);
			
		PlayerEvent.OnShoot += SetShootAnim;
		PlayerEvent.RecieveCash += AddCash;
        Stats.GetStat(StatType.MagCapacity).OnValueChange += CalculateMaxCap;
		Stats.GetStat(StatType.ShootSpeed).OnValueChange += SetShootingSpeedAnim;
		Stats.GetStat(StatType.Speed).OnValueChange += SetMovementSpeedAnim;
	}
    private void OnDestroy()
    {
        _hp.OnValueChange -= HandleHealthChange;
        _maxHp.OnValueChange -= HandleMaxHpChange;
		//InputEvent.OnInputSwitchGuns -= SwitchGun;

		PlayerEvent.OnShoot -= SetShootAnim;
		PlayerEvent.RecieveCash -= AddCash;
		Stats.GetStat(StatType.MagCapacity).OnValueChange -= CalculateMaxCap;
		Stats.GetStat(StatType.ShootSpeed).OnValueChange -= SetShootingSpeedAnim;
		Stats.GetStat(StatType.Speed).OnValueChange -= SetMovementSpeedAnim;
	}
	private void Start()
    {
        CalculateMaxCap();
		EquipGun(0);
        InitHealthBar();
        Cash = StartingCash;
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
        Weapons[CurrentWeaponIndex].gameObject.SetActive(false);
    }

    // Weapon handle
    public void InstantiateGun(WeaponBase gun, int index)
    {
	    if (Weapons[index] != null)
	    {
		    Stats.GetStat(StatType.Speed).RemoveModifier(new StatModifier(-Weapons[index].GunData.Weight,StatModType.Flat));
		    Destroy(Weapons[index].gameObject);
	    }
	    Weapons[index] = (Instantiate(gun, WeaponHoldPoint.transform));
	    Weapons[index].gameObject.layer = gameObject.layer;
	    CalculateMaxCap();
	    Weapons[index].Initialize();
	    Weapons[index].gameObject.SetActive(false);
	    EquipGun(CurrentWeaponIndex);
    }
    
    public void SwapGuns(int x, int y)
    {
	    (Weapons[x], Weapons[y]) = (Weapons[y], Weapons[x]);
	    EquipGun(CurrentWeaponIndex);
    }

    public void SetPlayerGuns(WeaponBase[] guns)
    {
	    for (int i = 0; i < Weapons.Length; i++)
	    {
		    InstantiateGun(guns[i],i);
	    }
    }
    public bool EquipGun(int index)
    {
	    WeaponBase currentSlot = Weapons[index];

	    if (currentSlot == null) return false;
        Stats.GetStat(StatType.Speed).RemoveModifier(new StatModifier(-Weapons[CurrentWeaponIndex].GunData.Weight,StatModType.Flat));
        Weapons[CurrentWeaponIndex].gameObject.SetActive(false);
        currentSlot.gameObject.SetActive(true);
        CurrentWeaponIndex = index;
        Animator.SetFloat("WeaponType", (float)currentSlot.GunData.WeaponType);
        Stats.GetStat(StatType.Speed).AddModifier(new StatModifier(-currentSlot.GunData.Weight,StatModType.Flat));
        PlayerEvent.OnEquipWeapon?.Invoke(currentSlot);
        return true;
    }

    public void SwitchWeapon(int index)
    {
	    if(!weaponSwitchable ||
	       CurrentWeaponIndex == index ||
	       Weapons[index]== null) return;
	    weaponSwitchable = false;
	    DOVirtual.DelayedCall(GunSwitchCooldown, () => { weaponSwitchable = true; });
	    //Switch
	    BeforeSwitching();
	    Animator.SetBool("SwitchWeapon", true);
	    Animator.SetBool("ReloadGun", false);
	    Weapons[CurrentWeaponIndex].OnSwitchOut();
	    EquipGun(index);
    }
    public void SwitchWeapon()
    {
        if(!weaponSwitchable ) return;
        weaponSwitchable = false;
        DOVirtual.DelayedCall(GunSwitchCooldown, () => { weaponSwitchable = true; });
        //Switch
		BeforeSwitching();
		Animator.SetBool("SwitchWeapon", true);
        Animator.SetBool("ReloadGun", false);
        
        Stats.GetStat(StatType.Speed).RemoveModifier(new StatModifier(-Weapons[CurrentWeaponIndex].GunData.Weight,StatModType.Flat));
        int n = 1;
        while (n < Weapons.Length)
        {
	        if(EquipGun((CurrentWeaponIndex+1)%Weapons.Length)) break;
	        n++;
        }
    }

    public bool ContainsWeapon(WeaponBase weapon)
    {
	    foreach (var x in Weapons)
	    {
		    if (x.Equals(weapon)) return true;
	    }

	    return false;
    }


	public void BeforeSwitching()
	{
		foreach (var weapon in Weapons)
		{
			if(!weapon) continue;
			weapon.ShootAble = false;
		}
		Weapons[CurrentWeaponIndex].OnSwitchOut();

	}
	public void AfterSwitching()
	{
		Weapons[CurrentWeaponIndex].OnSwitchIn();
		foreach (var weapon in Weapons)
		{
			if(!weapon) continue;
			weapon.ShootAble = true;
		}
	}
    public void AfterReload()
    {
        ((GunBase)Weapons[CurrentWeaponIndex]).Stats.GetAttribute(AttributeType.Bullets).SetValueToMax();
		PlayerEvent.OnReload?.Invoke();
		Animator.SetBool("ReloadGun", false);
        AfterSwitching();
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
        if(Weapons[CurrentWeaponIndex].GunData.WeaponType == WeaponType.Knife) return;
        GunBase gun = (GunBase)Weapons[CurrentWeaponIndex];
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
	    for (int i = 0; i < Weapons.Length; i++)
	    {
		    if(!Weapons[i]) continue;
		    if(Weapons[i].GunData.WeaponType == WeaponType.Knife) continue;
		    GunBase gun = (GunBase)Weapons[i];
		    gun.SetBulletCap(Stats.GetStat(StatType.MagCapacity).Value);
	    }

	    PlayerEvent.OnChangeCap?.Invoke();
    }
    //Cash
    public void AddCash(int amount)
    {
	    Cash += amount;
    }
}
