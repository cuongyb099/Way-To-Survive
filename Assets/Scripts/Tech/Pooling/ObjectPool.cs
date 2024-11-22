using System;
using System.Collections.Generic;
using System.Linq;
using Tech.Singleton;
using UnityEngine;

namespace Tech.Pooling
{
	public enum PoolType
	{
		ParticleSystem,
		GameObject,
		DamagePopUp,
		ENEMY,
		Audio,
		None
	}

	public class ObjectPool : Singleton<ObjectPool>
	{
		private List<PooledObject> objectPools = new ();

		private Dictionary<PoolType, GameObject> poolsHolder = new(); 
		
		protected override void Awake()
		{
			base.Awake();
			SetupHolder();
		}

		private void SetupHolder()
		{
			foreach (PoolType pool in Enum.GetValues(typeof(PoolType)))
			{
				if (pool == PoolType.None) continue;
				
				GameObject empty = new (pool.ToString());
				empty.transform.SetParent(transform);
				poolsHolder.Add(pool, empty);
			}
		}

		public GameObject SpawnObject(GameObject objectToSpawn, Vector3 position, Quaternion rotation, PoolType poolType = PoolType.None)
		{
			PooledObject pool = objectPools.Find(p => p.LookupString == objectToSpawn.name);

			if (pool == null)
			{
				pool = new PooledObject() { LookupString = objectToSpawn.name };
				objectPools.Add(pool);
			}

			GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();
			if (spawnableObj == null)
			{
				GameObject parentObject = SetParentObject(poolType);
				spawnableObj = Instantiate(objectToSpawn, position, rotation);

				if (parentObject != null)
				{
					spawnableObj.transform.SetParent(parentObject.transform);
				}
			}
			else
			{
				spawnableObj.transform.position = position;
				spawnableObj.transform.rotation = rotation;
				pool.InactiveObjects.Remove(spawnableObj);
			}
			spawnableObj.SetActive(true);

			return spawnableObj;
		}

		public void ReturnObjectToPool(GameObject obj)
		{
			string goName = obj.name.Substring(0, obj.name.Length - 7);
			PooledObject pool = objectPools.Find(p => p.LookupString == goName);

			if (pool == null)
			{
				Debug.LogWarning("Trying to release an object that is not pooled" + obj.name);
			}
			else
			{
				obj.SetActive(false);
				pool.InactiveObjects.Add(obj);
			}
		}

		private GameObject SetParentObject(PoolType poolType)
		{
			if (poolType == PoolType.None) return null;
			return poolsHolder[poolType];
		}
	}

	public class PooledObject
	{
		public string LookupString;
		public List<GameObject> InactiveObjects = new List<GameObject>();
	}
}