using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Asteroids.Game
{
	public class AsteroidFactory : MonoBehaviour
	{
		[SerializeField] private Asteroid _prefab;
		[SerializeField] private Vector2 _timeRange;

		private Vector2 _screenSize;
		private Queue<Asteroid> _pool;

		private void Start()
		{
			_pool = new Queue<Asteroid>();
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
			Asteroid newAsteroid = GetPrefab();

			newAsteroid.Init(_screenSize);
			newAsteroid.OnDisable += ReturnToPool;
		}

		private void ReturnToPool(Asteroid newAsteroid)
		{
			_pool.Enqueue(newAsteroid);
			newAsteroid.OnDisable -= ReturnToPool;
		}

		private Asteroid GetPrefab()
		{
			if (_pool.Count == 0)
			{
				Asteroid newAsteroid = Instantiate(_prefab, transform);
				return newAsteroid;
			}

			return _pool.Dequeue();
		}
	}
}
