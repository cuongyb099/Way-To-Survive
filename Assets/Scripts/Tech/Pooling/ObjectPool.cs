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
		private readonly Dictionary<GameObject, PooledObject> _objectPools = new ();
		private readonly Dictionary<PoolType, Transform> _poolsHolder = new(); 

		protected override void Awake()
		{
			base.Awake();
			SetupHolder();
		}

		private void SetupHolder()
		{
			var child = new Transform[transform.childCount];
	
			for (int i = 0; i < transform.childCount; i++)
			{
				child[i] = transform.GetChild(i);
			}

			foreach (PoolType pool in Enum.GetValues(typeof(PoolType)))
			{
				if (pool == PoolType.None) continue;
		
				var name = pool.ToString();
		
				Transform existTransform = child.FirstOrDefault(x => x.name == name);
		
				if (existTransform)
				{
					_poolsHolder.Add(pool, existTransform);
					continue;
				}
		
				GameObject empty = new (name);
				empty.transform.SetParent(transform);
				_poolsHolder.Add(pool, empty.transform);
			}
		}

		public GameObject SpawnObject(GameObject objectToSpawn, Vector3 position, Quaternion rotation, PoolType poolType = PoolType.None)
		{
			if (!_objectPools.ContainsKey(objectToSpawn))
			{
				_objectPools.Add(objectToSpawn, new PooledObject(objectToSpawn));
			}

			GameObject spawnableObj = _objectPools[objectToSpawn].GetPool(position, rotation);

			if (poolType != PoolType.None)
			{
				spawnableObj.transform.SetParent(GetParentObject(poolType).transform);
			}

			return spawnableObj;
		}

		public void ReturnObjectToPool(GameObject obj)
		{
			obj.gameObject.SetActive(false);
		}

		public Transform GetParentObject(PoolType poolType)
		{
			if (poolType == PoolType.None) return null;
			return _poolsHolder[poolType];
		}
	}

	public class PooledObject
	{
		private Stack<GameObject> _inActiveObjects = new ();
		private GameObject _baseObject;

		public PooledObject(GameObject obj)
		{
			_baseObject = obj;
		}

		public GameObject GetPool(Vector3 position, Quaternion rotation)
		{
			GameObject tmp;
	
			if (_inActiveObjects.Count > 0)
			{
				tmp = _inActiveObjects.Pop();
				tmp.transform.position = position;
				tmp.transform.rotation = rotation;
				tmp.SetActive(true);
				return tmp;
			}

			tmp = GameObject.Instantiate(_baseObject, position, rotation);
			return tmp;
		}

		public void AddToPool(GameObject obj)
		{
			_inActiveObjects.Push(obj);
		}
	}
}