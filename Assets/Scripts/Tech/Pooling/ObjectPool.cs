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
		None
	}

	public class ObjectPool : Singleton<ObjectPool>
	{
		private GameObject gameObjectEmpty;
		private GameObject particleSystemEmpty;
		[SerializeField] private GameObject dmgPopupEmpty;

		private List<PooledObject> ObjectPools = new List<PooledObject>();

		protected override void Awake()
		{
			base.Awake();
			SetupEmpties();
		}

		private void SetupEmpties()
		{
			gameObjectEmpty = new GameObject("GameObject");
			particleSystemEmpty = new GameObject("Particle Effect");

			gameObjectEmpty.transform.SetParent(transform);
			particleSystemEmpty.transform.SetParent(transform);
		}

		public GameObject SpawnObject(GameObject objectToSpawn, Vector3 position, Quaternion rotation, PoolType poolType = PoolType.None)
		{
			PooledObject pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

			if (pool == null)
			{
				pool = new PooledObject() { LookupString = objectToSpawn.name };
				ObjectPools.Add(pool);
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
				spawnableObj.SetActive(true);
			}

			return spawnableObj;
		}

		public void ReturnObjectToPool(GameObject obj)
		{
			string goName = obj.name.Substring(0, obj.name.Length - 7);
			PooledObject pool = ObjectPools.Find(p => p.LookupString == goName);

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
			return poolType switch
			{
				PoolType.GameObject => gameObjectEmpty,
				PoolType.ParticleSystem => particleSystemEmpty,
				PoolType.DamagePopUp => dmgPopupEmpty,
				PoolType.None => null,
				_ => null
			};
		}
	}

	public class PooledObject
	{
		public string LookupString;
		public List<GameObject> InactiveObjects = new List<GameObject>();
	}
}