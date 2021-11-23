using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Game
{
	public abstract class PoolableUnit : MonoBehaviour
	{
		private ObjectPool<PoolableUnit> _pool;

		public void Init(ObjectPool<PoolableUnit> pool)
		{
			_pool = pool;
		}

		protected void Release()
		{
			_pool.Release(this);
		}
	}
}
