using System.Collections;
using System.Collections.Generic;
using Tech.Pooling;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;
using Tech.Singleton;

public class DamagePopUpGenerator : Singleton<DamagePopUpGenerator>
{
    public GameObject PopUpPrefab;
	public GameObject CritPopUpPrefab;
	public GameObject CashPopUpPrefab;
	
	public void CreateDamagePopUp(Vector3 position,string text, bool crit = false)
    {
	    GameObject obj = ObjectPool.Instance.SpawnObject(crit?CritPopUpPrefab:PopUpPrefab, position, Quaternion.identity,PoolType.UIPopUp);
	    DamagePopUpController popup = obj.GetComponent<DamagePopUpController>();

        popup.Text.text = text;
    }
	public void CreateDamagePopUp(Vector3 position, DamageInfo damageInfo)
	{
		GameObject obj = ObjectPool.Instance.SpawnObject(damageInfo.IsCrit?CritPopUpPrefab:PopUpPrefab, position, Quaternion.identity,PoolType.UIPopUp);
		DamagePopUpController popup = obj.GetComponent<DamagePopUpController>();
            
		popup.Text.text = ((int)damageInfo.Damage).ToString();
	}
	public void CreateCashPopUp(Vector3 position, string text)
	{
		GameObject obj = ObjectPool.Instance.SpawnObject(CashPopUpPrefab, position, Quaternion.identity,PoolType.UIPopUp);
		DamagePopUpController popup = obj.GetComponent<DamagePopUpController>();

		popup.Text.text = text;

	}
}
