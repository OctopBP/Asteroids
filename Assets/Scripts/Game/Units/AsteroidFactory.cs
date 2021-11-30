using System.Collections.Generic;
using System.Threading.Tasks;
using Asteroids.Data;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Game
{
	public class AsteroidFactory : MonoBehaviour
	{
		[SerializeField] private Asteroid _prefab;
		[SerializeField] private SmallAsteroid _smallPrefab;
		[SerializeField] private Vector2 _timeRange;

		private GameData _gameData;
		private ObjectPool<Asteroid> _pool;
		private ObjectPool<SmallAsteroid> _smallPool;
		private UnitFactory _unitFactory;
		private float _timer;

		private readonly List<Asteroid> _asteroidsInField = new();
		private readonly List<Asteroid> _smallAsteroidsInField = new();

		public void Init(GameData gameData, UnitFactory unitFactory)
		{
			_pool = new ObjectPool<Asteroid>(CreateAsteroid, Activate, Deactivate, Destroy, false, 10, 100);
			_smallPool =
				new ObjectPool<SmallAsteroid>(CreateSmallAsteroid, Activate, Deactivate, Destroy, false, 10, 100);

			_gameData = gameData;
			_unitFactory = unitFactory;

			ClearAsteroids();
			ClearSmallAsteroids();
		}

		private void ClearAsteroids()
		{
			_asteroidsInField.ForEach(asteroid =>
			{
				_pool.Release(asteroid);
				asteroid.OnDisable -= ReturnToPool;
			});
			_asteroidsInField.Clear();
		}

		private void ClearSmallAsteroids()
		{
			_smallAsteroidsInField.ForEach(asteroid =>
			{
				_smallPool.Release(asteroid as SmallAsteroid);
				asteroid.OnDisable -= ReturnToSmall;
			});
			_smallAsteroidsInField.Clear();
		}


		public void Tick()
		{
			if (_timer <= 0)
				Spawn();
			else
				_timer -= Time.deltaTime;
		}

		private void Spawn()
		{
			SpawnAsteroid();
			_timer = GetRandomTime();
		}

		private float GetRandomTime()
		{
			return Random.Range(_timeRange.x, _timeRange.y);
		}

		private void SpawnAsteroid()
		{
			Asteroid newAsteroid = _pool.Get();

			_unitFactory.SetupItem(newAsteroid);

			newAsteroid.Init(_gameData);
			newAsteroid.OnDisable += ReturnToPool;

			_asteroidsInField.Add(newAsteroid);
		}

		private void SpawnSmallAsteroid(Asteroid asteroid)
		{
			SmallAsteroid newAsteroid = _smallPool.Get();

			newAsteroid.Init(_gameData, asteroid.transform.position, asteroid.Velocity);
			newAsteroid.OnDisable += ReturnToSmall;

			_asteroidsInField.Add(newAsteroid);
		}

		private void ReturnToPool(Asteroid asteroid)
		{
			for (int i = 0; i < asteroid.SubAsteroids; i++)
				SpawnSmallAsteroid(asteroid);

			_pool.Release(asteroid);
			asteroid.OnDisable -= ReturnToPool;
			_asteroidsInField.Remove(asteroid);
		}

		private void ReturnToSmall(Asteroid asteroid)
		{
			_smallPool.Release(asteroid as SmallAsteroid);
			asteroid.OnDisable -= ReturnToSmall;
			_asteroidsInField.Remove(asteroid);
		}


		private Asteroid CreateAsteroid() => Instantiate(_prefab, transform);
		private SmallAsteroid CreateSmallAsteroid() => Instantiate(_smallPrefab, transform);
		private void Activate(Asteroid asteroid) => asteroid.gameObject.SetActive(true);
		private void Deactivate(Asteroid asteroid) => asteroid.gameObject.SetActive(false);
		private void Destroy(Asteroid asteroid) => Destroy(asteroid.gameObject);
	}
}