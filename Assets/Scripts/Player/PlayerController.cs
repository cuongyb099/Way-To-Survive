using System;
using DG.Tweening;
using ResilientCore;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

public class PlayerController : BasicController
{
	#region AnimationID
	private static readonly int PlayerHit = Animator.StringToHash("PlayerHit");
	private static readonly int Type = Animator.StringToHash("WeaponType");
	private static readonly int SwitchCurWeapon = Animator.StringToHash("SwitchWeapon");
	private static readonly int ReloadGun = Animator.StringToHash("ReloadGun");
	private static readonly int ShootingSpeed = Animator.StringToHash("ShootingSpeed");
	private static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");
	private static readonly int Shoot = Animator.StringToHash("Shoot");
	private static readonly int PosX = Animator.StringToHash("PosX");
	private static readonly int PosY = Animator.StringToHash("PosY");
	#endregion
	public Rigidbody Rigidbody { get; private set; }
    public FloatingCapsule FloatingCapsule { get; private set; }
    public CapsuleCollider Collider { get; private set; }
    public Animator Animator { get; private set; }
    [field:SerializeField] public CameraZoom CameraZoom { get; private set; }
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
    public Transform RightHandHoldPoint;
    public Transform LeftHandHoldPoint;
    public LineRendererHelper LineRendererL;
    public LineRendererHelper LineRendererR;
    public WeaponBase[] Weapons;
    [field: SerializeField] public BoxCollider MeleeHitCollider { get; private set; }  
    public WeaponBase CurrentWeapon => Weapons[CurrentWeaponIndex];
    public int CurrentWeaponIndex { get; private set; }

    private bool weaponSwitchable = true;
    private Camera mainCamera;
	//Unity and override methods
    protected override void Awake()
    {
        base.Awake();
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<CapsuleCollider>();
        FloatingCapsule = GetComponent<FloatingCapsule>();
        Animator = GetComponentInChildren<Animator>();
        PlayerInteractor = GetComponentInChildren<PlayerInteractor>();
        mainCamera = Camera.main;
        
        BuffList = new List<int>();
        OwnedWeapons = new List<WeaponBase> { StartingWeapon };

        Weapons = new WeaponBase[3];
		InstantiateWeapon(StartingWeapon, 0);
			
		PlayerEvent.OnAttack += SetShootAnim;
		PlayerEvent.RecieveCash += AddCash;
        Stats.GetStat(StatType.MagCapacity).OnValueChange += CalculateMaxCap;
		Stats.GetStat(StatType.ATKSpeed).OnValueChange += SetShootingSpeedAnim;
		Stats.GetStat(StatType.Speed).OnValueChange += SetMovementSpeedAnim;
	}
    private void OnDestroy()
    {
        hp.OnValueChange -= HandleHealthChange;
        maxHp.OnValueChange -= HandleMaxHpChange;
		//InputEvent.OnInputSwitchGuns -= SwitchGun;

		PlayerEvent.OnAttack -= SetShootAnim;
		PlayerEvent.RecieveCash -= AddCash;
		Stats.GetStat(StatType.MagCapacity).OnValueChange -= CalculateMaxCap;
		Stats.GetStat(StatType.ATKSpeed).OnValueChange -= SetShootingSpeedAnim;
		Stats.GetStat(StatType.Speed).OnValueChange -= SetMovementSpeedAnim;
	}
	private void Start()
    {
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

    public override float Damage(DamageInfo info)
    {
	    Animator.SetTrigger(PlayerHit);
	    return base.Damage(info);
    }

    // Weapon handle
    public void InstantiateWeapon(WeaponBase weapon, int index)
    {
	    if (Weapons[index] != null)
	    {
		    Stats.GetStat(StatType.Speed).RemoveModifier(new StatModifier(-Weapons[index].WeaponData.Weight,StatModType.Flat));
		    Destroy(Weapons[index].gameObject);
	    }
	    Weapons[index] = (Instantiate(weapon, RightHandHoldPoint.transform));
	    Weapons[index].gameObject.layer = gameObject.layer;
	    Weapons[index].Initialize();
	    Weapons[index].gameObject.SetActive(false);
	    
	    CurrentWeapon.ShootAble = true;
	    Animator.SetBool(ReloadGun, false);
	    Animator.SetBool(SwitchCurWeapon, false);
	    
	    //if weapon is a gun
	    if (Weapons[index] is GunBase gun)
	    {
		    CalculateMaxCapacity(Weapons[index]);
		    gun.SetBulletToMax();
	    }
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
		    InstantiateWeapon(guns[i],i);
	    }
    }
    public bool EquipGun(int index)
    {
	    WeaponBase currentSlot = Weapons[index];

	    if (currentSlot == null) return false;
        Stats.GetStat(StatType.Speed).RemoveModifier(new StatModifier(-Weapons[CurrentWeaponIndex].WeaponData.Weight,StatModType.Flat));
        Weapons[CurrentWeaponIndex].gameObject.SetActive(false);
        currentSlot.gameObject.SetActive(true);
        CurrentWeaponIndex = index;
        CalculateMaxCapacity(Weapons[index]);
        Animator.SetFloat(Type, (float)currentSlot.WeaponData.WeaponType);
        Animator.SetBool(ReloadGun, false);
        Stats.GetStat(StatType.Speed).AddModifier(new StatModifier(-currentSlot.WeaponData.Weight,StatModType.Flat));
        CameraZoom.SetZoom(CurrentWeapon.WeaponData.Aim/Mathf.Cos(45f*Mathf.Deg2Rad)+5f);
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
	    Animator.SetBool(SwitchCurWeapon, true);
	    Animator.SetBool(ReloadGun, false);
	    Weapons[CurrentWeaponIndex].OnSwitchOut();
	    EquipGun(index);
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
        ((GunBase)Weapons[CurrentWeaponIndex]).SetBulletToMax();
		PlayerEvent.OnReload?.Invoke();
		Animator.SetBool(ReloadGun, false);
        AfterSwitching();
	}
    public void SetShootingSpeedAnim()
    {
		Animator.SetFloat(ShootingSpeed,Stats.GetStat(StatType.ATKSpeed).Value);
	}
	public void SetMovementSpeedAnim()
	{
		float value = Stats.GetStat(StatType.Speed).Value;
		float baseValue = Stats.GetStat(StatType.Speed).BaseValue;
		Animator.SetFloat(MovementSpeed, value>baseValue?(value/baseValue):1f);
	}
	public void SetShootAnim()
	{
		Animator.SetTrigger(Shoot);
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

	public Gradient LineDefaultColor;
	public Gradient LineTargetColor;
	public void SetLineRenderers()
	{
        if(Weapons[CurrentWeaponIndex].WeaponData.WeaponType == WeaponType.Knife) return;
        GunBase gun = (GunBase)Weapons[CurrentWeaponIndex];
        float accuracy = gun.GunAccuracy;
		LineRendererL.SetLineRenderer(gun.ShootPoint, gun.WeaponData.Aim, Quaternion.Euler(0, Mathf.Clamp(-accuracy, -GameValues.RecoilMaxValue,0), 0) * transform.forward);
		LineRendererR.SetLineRenderer(gun.ShootPoint, gun.WeaponData.Aim, Quaternion.Euler(0, Mathf.Clamp(accuracy, 0, GameValues.RecoilMaxValue), 0) * transform.forward);
		
        if(Physics.Raycast(gun.ShootPoint.position,transform.forward, out RaycastHit hit, gun.WeaponData.Aim) &&
           accuracy <= 1f)
        {
	        if (hit.collider.CompareTag("Enemy"))
	        {
		        LineRendererL.LR.colorGradient = LineTargetColor;
		        LineRendererR.LR.colorGradient = LineTargetColor;
	        }
	        
	        return;
        }
        
        LineRendererL.LR.colorGradient = LineDefaultColor;
        LineRendererR.LR.colorGradient = LineDefaultColor;

	}

	// Movement
	Vector2 CurrentBlend;
    private void MovePlayer()
    {
        Vector3 MovementInput = PlayerInput.Instance.MovementInput;
        Rigidbody.velocity = new Vector3(0f, Rigidbody.velocity.y, 0f);
        Rigidbody.AddForce(Quaternion.Euler(0,mainCamera.transform.eulerAngles.y,0) * MovementInput *  (Stats.GetStat(StatType.Speed).Value), ForceMode.VelocityChange);
        //Animation
        var x =Vector3.Dot(MovementInput, Quaternion.Euler(0,-mainCamera.transform.eulerAngles.y,0)* transform.right);
        var y =Vector3.Dot(MovementInput, Quaternion.Euler(0,-mainCamera.transform.eulerAngles.y,0)* transform.forward);
        CurrentBlend = Vector2.Lerp(CurrentBlend, new Vector2(x,y) , 0.1f);
        Animator.SetFloat(PosX,CurrentBlend.x*Stats.GetStat(StatType.Speed).Value/Stats.GetStat(StatType.Speed).BaseValue);
        Animator.SetFloat(PosY,CurrentBlend.y*Stats.GetStat(StatType.Speed).Value/Stats.GetStat(StatType.Speed).BaseValue);
    }

    public void RotatePlayer()
    {
        Vector2 rotateInput = PlayerInput.Instance.RotationInput;
        if (rotateInput == Vector2.zero) return;
        float atan = Mathf.Atan2(rotateInput.x,rotateInput.y)*Mathf.Rad2Deg;
        Rigidbody.rotation = Quaternion.Euler(0,atan+mainCamera.transform.eulerAngles.y,0);
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
    private Attribute hp;
    private Stat maxHp;
    private void InitHealthBar()
    {
        if (Stats.TryGetAttribute(AttributeType.Hp, out hp))
        {
            hp.OnValueChange += HandleHealthChange;
            PlayerEvent.OnInitStatusBar?.Invoke(AttributeType.Hp, hp.Value, hp.MaxValue);
        }

        if (Stats.TryGetStat(StatType.MaxHP, out maxHp))
        {
            maxHp.OnValueChange += HandleMaxHpChange;
        }
    }

    private void HandleMaxHpChange()
    {
        PlayerEvent.OnMaxHeathChange?.Invoke(hp.Value, maxHp.Value);
    }

    private void HandleHealthChange()
    {
        if (hp == null) return;
        PlayerEvent.OnHeathChange?.Invoke(hp.Value, hp.MaxValue);
    }
    //Buffs
    public List<int> BuffList { get; private set; }
    public void AddBuffToPlayer(BaseBuffSO buff)
    {
		BaseStatusEffect buffEffect = buff.AddStatusEffect(Stats);
        BuffList.Add(buff.ID);
        buffEffect.OnEnd += () => BuffList.Remove(buff.ID);
    }

    public void CalculateMaxCap()
    {
	    for (int i = 0; i < Weapons.Length; i++)
	    {
		    if(!Weapons[i]) continue;
		    if(Weapons[i] is GunBase gun)
				gun.SetBulletCap(Stats.GetStat(StatType.MagCapacity).Value);
	    }

	    PlayerEvent.OnChangeCap?.Invoke();
    }

    public void CalculateMaxCapacity(WeaponBase weaponBase)
    {
	    if(!weaponBase) return;
	    if(weaponBase is GunBase gun)
			gun.SetBulletCap(Stats.GetStat(StatType.MagCapacity).Value);
    }
    //Cash
    public void AddCash(int amount)
    {
	    DamagePopUpGenerator.Instance.CreateCashPopUp(transform.position, $"+{amount} $");
	    Cash += amount;
    }
}
