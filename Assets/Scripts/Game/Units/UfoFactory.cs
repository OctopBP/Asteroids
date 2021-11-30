using System.Collections.Generic;
using System.Threading.Tasks;
using Asteroids.Data;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Game
{
	public class UfoFactory : MonoBehaviour
	{
		[SerializeField] private Ufo _prefab;
		[SerializeField] private Vector2 _timeRange;

		private GameData _gameData;
		private UnitFactory _unitFactory;
		private Spaceship _spaceship;
		private ObjectPool<Ufo> _pool;
		private float _timer;

		private readonly List<Ufo> _ufoInField = new();

		public void Init(GameData gameData, UnitFactory unitFactory, Spaceship spaceship)
		{
			_pool = new ObjectPool<Ufo>(CreateUfo, Activate, Deactivate, Destroy, false, 10, 100);

			_gameData = gameData;
			_unitFactory = unitFactory;
			_spaceship = spaceship;

			ClearUfo();
		}

		public void Tick()
		{
			if (_timer <= 0)
				Spawn();
			else
				_timer -= Time.deltaTime;
		}

		private void ClearUfo()
		{
			_ufoInField.ForEach(asteroid =>
			{
				_pool.Release(asteroid);
				asteroid.OnDisable -= ReturnToPool;
			});
			_ufoInField.Clear();
		}

		private void Spawn()
		{
			SpawnUfo();
			_timer = GetRandomTime();
		}

		private float GetRandomTime()
		{
			return Random.Range(_timeRange.x, _timeRange.y);
		}

		private void SpawnUfo()
		{
			Ufo newUfo = _pool.Get();

			_unitFactory.SetupItem(newUfo);

			newUfo.Init(_spaceship.transform);
			newUfo.OnDisable += ReturnToPool;

			_ufoInField.Add(newUfo);
		}

		private void ReturnToPool(Ufo ufo)
		{
			_pool.Release(ufo);
			ufo.OnDisable -= ReturnToPool;
			_ufoInField.Remove(ufo);
		}

		private Ufo CreateUfo() => Instantiate(_prefab, transform);
		private void Activate(Ufo ufo) => ufo.gameObject.SetActive(true);
		private void Deactivate(Ufo ufo) => ufo.gameObject.SetActive(false);
		private void Destroy(Ufo ufo) => Destroy(ufo.gameObject);
	}
}