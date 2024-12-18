using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;
using Tech.Singleton;

public class DamagePopUpGenerator : Singleton<DamagePopUpGenerator>
{
    public DamagePopUpController PopUpPrefab;
	public DamagePopUpController CritPopUpPrefab;
	public ObjectPool<DamagePopUpController> PopUpNonCritPool;
	public ObjectPool<DamagePopUpController> PopUpCritPool;
	protected override void Awake()
	{
		base.Awake();
		PopUpNonCritPool = new ObjectPool<DamagePopUpController>(() => {
			var obj = Instantiate(PopUpPrefab, transform);
			obj.StopAnimation += () => PopUpNonCritPool.Release(obj);
			return obj;
		}, obj => {
			obj.Get();
		}, obj => {
			obj.Release();
		}, obj => {
			Destroy(obj.gameObject);
		}, true, 100);

		PopUpCritPool = new ObjectPool<DamagePopUpController>(() => {
			var obj = Instantiate(CritPopUpPrefab, transform);
			obj.StopAnimation += () => PopUpCritPool.Release(obj);
			return obj;
		}, obj => {
			obj.Get();
		}, obj => {
			obj.Release();
		}, obj => {
			Destroy(obj.gameObject);
		}, true, 100);
	}
	public void CreateDamagePopUp(Vector3 position,string text, bool crit)
    {
		DamagePopUpController popup = crit? PopUpCritPool.Get(): PopUpNonCritPool.Get();
            
        var tmp = popup.GetComponentInChildren<TMP_Text>();
		popup.transform.position = position;
        tmp.text = text;
		popup.ResetPopUp();
    }
	public void CreateDamagePopUp(Vector3 position, DamageInfo damageInfo)
	{
		DamagePopUpController popup = damageInfo.IsCrit ? PopUpCritPool.Get() : PopUpNonCritPool.Get();

		var tmp = popup.GetComponentInChildren<TMP_Text>();
		popup.transform.position = position;
		tmp.text = ((int)damageInfo.Damage).ToString();
		popup.ResetPopUp();
	}
}
