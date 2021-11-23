using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Asteroids.Game
{
	public class UnitFactory : MonoBehaviour
	{
		[SerializeField] private PoolableUnit _prefab;
		[SerializeField] private Vector2 _timeRange;
		[SerializeField] private int _defaultCapacity = 10;
		[SerializeField] private int _maxSize = 100;

		private Vector2 _screenSize;
		private ObjectPool<PoolableUnit> _pool;

		private void Start()
		{
			_pool = new ObjectPool<PoolableUnit>(CreateItem, Activate, Deactivate, Destroy, false, _defaultCapacity, _maxSize);
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
				SpawnItem();
				await WaitRandomTime();
			}
		}

		private async Task WaitRandomTime()
		{
			float randomTime = Random.Range(_timeRange.x, _timeRange.y);
			int milliseconds = Mathf.RoundToInt(randomTime * 1000);
			await Task.Delay(milliseconds);
		}

		private void SpawnItem()
		{
			PoolableUnit newItem = _pool.Get();
			newItem.Init(_pool);
		}

		private PoolableUnit CreateItem() => Instantiate(_prefab, transform);
		private void Activate(PoolableUnit item) => item.gameObject.SetActive(true);
		private void Deactivate(PoolableUnit item) => item.gameObject.SetActive(false);
		private void Destroy(PoolableUnit item) => Destroy(item.gameObject);
	}
}
