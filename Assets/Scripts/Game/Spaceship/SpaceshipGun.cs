using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Asteroids.Game
{
	public class SpaceshipGun : MonoBehaviour, ISpaceshipGun
	{
		[SerializeField] private GameObject _bulletPrefab;
		[SerializeField] private float _cooldownTime = 0.3f;

		private Queue<GameObject> _bullets;
		private float _cooldown;

		private bool _canShoot => _cooldown > 0;

		private void Start()
		{
			_bullets = new Queue<GameObject>();
		}

		public void Fire()
		{
			if (_canShoot) return;
			_cooldown = _cooldownTime;

			GameObject newBullet = Instantiate(_bulletPrefab);
			newBullet.transform.position = transform.position;
			newBullet.transform.rotation = transform.rotation;

			Timer();
		}

		private async void Timer()
		{
			while (_cooldown > 0)
			{
				_cooldown -= Time.deltaTime;
				await Task.Yield();
			}
		}
	}
}
