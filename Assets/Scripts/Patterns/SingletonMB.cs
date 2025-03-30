using UnityEngine;

namespace Merge.Patterns
{
	public abstract class SingletonMB<T> : MonoBehaviour where T : SingletonMB<T>
	{
		public static T Instance { get; private set; }

		private void Awake()
		{
			if (Instance != null)
			{
				Destroy(this);
			}

			Instance = this as T;

			SingletonAwake();
		}

		protected abstract void SingletonAwake();
	}
}
