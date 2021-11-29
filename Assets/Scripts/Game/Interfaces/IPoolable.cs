using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Game
{
	public abstract class PoolableUnit : MonoBehaviour
	{
		[SerializeField] private Vector2 _sizeRange;

		public float Size => Random.Range(_sizeRange.x, _sizeRange.y);

		private ObjectPool<PoolableUnit> _pool;
		private Vector2 _screenSize;

		protected abstract void DestroyUnit();

		private void Update()
		{
			CheckOutOfScreen();
		}

		public void SetScreenSize(Vector2 screenSize)
		{
			_screenSize = screenSize;
		}

		private void CheckOutOfScreen()
		{
			if (transform.position.x > _screenSize.x / 2
			    || transform.position.x < -_screenSize.x / 2
			    || transform.position.y > _screenSize.y / 2
			    || transform.position.y < -_screenSize.y / 2)
			{
				DestroyUnit();
			}
		}
	}
}