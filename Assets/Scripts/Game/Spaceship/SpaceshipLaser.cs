using System.Threading.Tasks;
using UnityEngine;
using Asteroids.Data;

namespace Asteroids.Game
{
	public class SpaceshipLaser : MonoBehaviour, ISpaceshipGun
	{
		[SerializeField] private LaserRay _laserRay;

		[Header("Time"), SerializeField] private float _shotDelay = 0.5f;
		[SerializeField] private float _cooldownTime = 2;
		[SerializeField] private int _maxBullets = 3;

		private GameData _gameData;
		
		private int _bullets;
		
		private float _cooldown;
		private float _reloadCooldown;
		private bool CanShoot => _cooldown <= 0 && _bullets > 0;

		public void Init(GameData gameData)
		{
			_gameData = gameData;
			ChangeBulletsCount(_maxBullets);
		}

		private void ChangeBulletsCount(int value)
		{
			_bullets = value;
			_gameData.SpaceshipData.SetBulletCount(value);
		}

		public void Fire()
		{
			if (!CanShoot) return;

			ChangeBulletsCount(_bullets - 1);

			Delay();
			Cooldown();
			
			_laserRay.Release();
		}

		private async void Delay()
		{
			_cooldown = _shotDelay;
			
			while (_cooldown > 0)
			{
				_cooldown -= Time.deltaTime;
				await Task.Yield();
			}
		}

		private async void Cooldown()
		{
			_reloadCooldown = _cooldownTime;
			
			while (_reloadCooldown > 0)
			{
				_reloadCooldown -= Time.deltaTime;
				await Task.Yield();
			}
			
			ChangeBulletsCount(_bullets + 1);

			if (_bullets < _maxBullets)
				Cooldown();
		}
	}
}
