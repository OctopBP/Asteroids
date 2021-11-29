using System;
using UnityEngine;

namespace Asteroids.Game
{
	public class Ufo : PoolableUnit, IShootable
	{
		[SerializeField] private float _speed;

		public event Action<Ufo> OnDisable;

		private Transform _target;
		private Vector3 _velocity;


		private void Update()
		{
			Move();
		}

		public void Init(Transform target)
		{
			_target = target;
			gameObject.SetActive(true);
		}

		public void OnShot()
		{
			DestroyUnit();
		}

		private void Move()
		{
			if (_target == null || !_target.gameObject.activeSelf) return;

			Vector3 direction = _target.position - transform.position;
			_velocity = _speed * direction.normalized;
			transform.position += _velocity * Time.deltaTime;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.TryGetComponent(out ICollisionRactive collidable)) return;
			collidable.OnCollision(_velocity);
		}

		protected override void DestroyUnit()
		{
			if (!gameObject.activeSelf) return;

			OnDisable?.Invoke(this);
			gameObject.SetActive(false);
		}
	}
}