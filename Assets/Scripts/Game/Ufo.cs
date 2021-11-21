using System;
using Asteroids.Utils;
using UnityEngine;

namespace Asteroids.Game
{
	public class Ufo : PoolableUnit, IShootable
	{
		[SerializeField] private float _speed;

		public event Action<Ufo> OnDisable;

		private Vector2 _screenSize;
		private Transform _target;

		private void Update()
		{
			Move();
			CheckOutOfScreen();
		}

		public void Init()
		{

		}

		public void Init(Vector2 screenSize)
		{
			gameObject.SetActive(true);
			_screenSize = screenSize;
			SetPosition();
		}

		public void OnShotted()
		{
			DestroyUfo();
		}

		private void Move()
		{
			Vector3 direction = transform.position - _target.position;
			transform.position += _speed * direction * Time.deltaTime;
		}

		private void CheckOutOfScreen()
		{
			if (transform.position.x > _screenSize.x / 2
				|| transform.position.x < -_screenSize.x / 2
				|| transform.position.y > _screenSize.y / 2
				|| transform.position.y < -_screenSize.y / 2)
			{
				DestroyUfo();
			}
		}

		private void DestroyUfo()
		{
			OnDisable?.Invoke(this);
			gameObject.SetActive(false);
		}

		private void SetPosition()
		{
			Vector2 position = new Vector2(GetPos(_screenSize.x), GetPos(_screenSize.y));
			transform.position = position;

			float GetPos(float value) => (value / 2) * Extensions.RandomSign();
		}
	}
}
