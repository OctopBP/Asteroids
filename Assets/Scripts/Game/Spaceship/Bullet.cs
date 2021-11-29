using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Game
{
	public class Bullet : MonoBehaviour
	{
		[SerializeField] private float _lifeTime = 1;

		private IObjectPool<Bullet> _pool;
		private float _createTime;

		public void Init(IObjectPool<Bullet> pool, Transform spawnPoint)
		{
			_pool = pool;
			_createTime = Time.time;

			transform.position = spawnPoint.position;
			transform.rotation = spawnPoint.rotation;
		}

		private void Update()
		{
			Move();
			CheckLifeTime();
		}

		private void Move()
		{
			transform.position += transform.up * Time.deltaTime * 10;
		}

		private void CheckLifeTime()
		{
			if (_createTime + _lifeTime < Time.time)
				ReturnToPool();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.TryGetComponent<IShootable>(out IShootable shootable)) return;
			shootable.OnShot();
			ReturnToPool();
		}

		private void ReturnToPool()
		{
			_pool.Release(this);
		}
	}
}