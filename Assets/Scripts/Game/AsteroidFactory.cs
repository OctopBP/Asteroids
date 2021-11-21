using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Game
{
	public class AsteroidFactory : MonoBehaviour
	{
		[SerializeField] private Asteroid _prefab;
		[SerializeField] private Vector2 _timeRange;

		private Vector2 _screenSize;
		private ObjectPool<Asteroid> _pool;

		private void Start()
		{
			_pool = new ObjectPool<Asteroid>(CreateAsteroid, Activate, Deactivate, Destroy, false, 10, 100);
		}

		public void Init(Vector2 screenSize)
		{
			_screenSize = screenSize;
			Spawn();
		}

		private async void Spawn()
		{
			while (Application.isPlaying)
			{
				SpawnAsteroid();
				await WaitRandomTime();
			}
		}

		private async Task WaitRandomTime()
		{
			float randomTime = Random.Range(_timeRange.x, _timeRange.y);
			int milliseconds = Mathf.RoundToInt(randomTime * 1000);
			await Task.Delay(milliseconds);
		}

		private void SpawnAsteroid()
		{
			Asteroid newAsteroid = _pool.Get();

			newAsteroid.Init(_screenSize);
			newAsteroid.OnDisable += ReturnToPool;
		}

		private void ReturnToPool(Asteroid newAsteroid)
		{
			_pool.Release(newAsteroid);
			newAsteroid.OnDisable -= ReturnToPool;
		}

		private Asteroid CreateAsteroid() => Instantiate(_prefab, transform);
		private void Activate(Asteroid asteroid) => asteroid.gameObject.SetActive(true);
		private void Deactivate(Asteroid asteroid) => asteroid.gameObject.SetActive(false);
		private void Destroy(Asteroid asteroid) => Destroy(asteroid.gameObject);
	}
}
