using System.Threading.Tasks;
using Asteroids.Data;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Game
{
	public class SpaceshipGun : MonoBehaviour, ISpaceshipGun
	{
		[SerializeField] private Bullet _bulletPrefab;
		[SerializeField] private Transform _container;
		[SerializeField] private float _cooldownTime = 0.3f;

		private ObjectPool<Bullet> _bullets;
		private float _cooldown;

		private bool _canShoot => _cooldown > 0;

		private void Start()
		{
			_bullets = new ObjectPool<Bullet>(CreateBullet, Activate, Deactivate, Destroy, false, 10, 100);
		}

		public void Init(GameData _) { }

		public void Fire()
		{
			if (_canShoot) return;
			_cooldown = _cooldownTime;

			Bullet newBullet = _bullets.Get();
			newBullet.Init(_bullets, transform);

			Timer();
		}

		private Bullet CreateBullet() => Instantiate(_bulletPrefab, _container);
		private void Activate(Bullet bullet) => bullet.gameObject.SetActive(true);
		private void Deactivate(Bullet bullet) => bullet.gameObject.SetActive(false);
		private void Destroy(Bullet bullet) => Destroy(bullet.gameObject);

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
