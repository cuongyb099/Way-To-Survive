using UnityEngine;

namespace Tech.Singleton
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		[SerializeField] protected bool dontDestroyOnLoad = true;
		private static T instance;
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = FindObjectOfType<T>();
					if (instance == null)
					{
						GameObject singleton = new GameObject(typeof(T).Name);
						instance = singleton.AddComponent<T>();
						DontDestroyOnLoad(singleton);
					}
				}
				return instance;
			}
		}

		protected virtual void Awake()
		{
			if (instance == null)
			{
				instance = this as T;
				if (dontDestroyOnLoad)
				{
					var root = transform.root;
					if (root != transform)
					{
						DontDestroyOnLoad(root);
					}
					else
					{
						DontDestroyOnLoad(this.gameObject);
					}
				}
			}
			else
			{
				Debug.LogError("Only 1 " + typeof(T).Name + " allow to exist");
			}
		}
	}
}
