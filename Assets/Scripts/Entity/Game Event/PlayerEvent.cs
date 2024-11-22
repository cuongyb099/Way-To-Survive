using System;

public static class PlayerEvent
{
    public static Action<float, float> OnHeathChange;
    public static Action<float, float> OnMaxHeathChange;
    public static Action<float, float> OnMaxManaChange;
    public static Action<float, float> OnManaChange;
	public static Action<AttributeType, float ,float> OnInitStatusBar;
	// Gun
	public static Action<GunBase> OnEquipWeapon;
	public static Action OnShoot;
	public static Action OnReload;
	public static Action OnChangeCap;
    public static Action<int> OnCashChange;
    public static Action<int> RecieveCash;
}
