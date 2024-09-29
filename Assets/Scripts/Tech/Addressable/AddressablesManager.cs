using System;
using System.Collections;
using System.Collections.Generic;
using Tech.Singleton;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesManager : Singleton<AddressablesManager>
{
	private readonly Dictionary<string, AsyncOperationHandle> dicAsset = new();

	public void CreateAsset<T>(string key, Action<T> onComplete, Action onFailed = null)
	{
		if (dicAsset.ContainsKey(key))
		{
			onComplete?.Invoke((T)(dicAsset[key].Result));
		}
		else
		{
			StartCoroutine(LoadAsset(key, onComplete, onFailed));
		}
	}

	private IEnumerator LoadAsset<T>(string key, Action<T> onComplete, Action onFailed = null)
	{
		var opHandle = Addressables.LoadAssetAsync<T>(key);
		yield return opHandle;

		if (opHandle.Status == AsyncOperationStatus.Succeeded)
		{
			onComplete?.Invoke(opHandle.Result);
			if (dicAsset.ContainsKey(key))
			{
				RemoveAsset(key);
			}
			dicAsset[key] = opHandle;
		}
		else if (opHandle.Status == AsyncOperationStatus.Failed)
		{
			Debug.LogError($"Load Asset Failed: {key}");
			onFailed?.Invoke();
		}
	}

	public void RemoveAsset(string key)
	{
		Addressables.Release(dicAsset[key]);
		dicAsset.Remove(key);
	}
}
