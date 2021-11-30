using System.Collections;
using Asteroids.Data;
using UnityEngine;

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
			_bullets = Mathf.Min(value, _maxBullets);
			_gameData.SpaceshipData.SetBulletCount(_bullets);
		}

		public void Fire()
		{
			if (!CanShoot) return;

			ChangeBulletsCount(_bullets - 1);

			Delay();
			Cooldown();

			_laserRay.Release();
		}

		private void Delay()
		{
			_cooldown = _shotDelay;
			StartCoroutine(DelayCor());
		}

		private void Cooldown()
		{
			if (_reloadCooldown > 0) return;

			_reloadCooldown = _cooldownTime;
			StartCoroutine(CooldownCor());
		}

		private IEnumerator DelayCor()
		{
			while (_cooldown > 0)
			{
				_cooldown -= Time.deltaTime;
				yield return null;
			}
		}

		private IEnumerator CooldownCor()
		{
			while (_reloadCooldown > 0)
			{
				_reloadCooldown -= Time.deltaTime;
				_gameData.SpaceshipData.SetCooldown(_reloadCooldown);
				yield return null;
			}

			ChangeBulletsCount(_bullets + 1);

			if (_bullets < _maxBullets)
				Cooldown();
		}
	}
}