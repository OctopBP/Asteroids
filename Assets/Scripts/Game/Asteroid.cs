using System;
using System.Collections.Generic;
using Asteroids.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Game
{
	public class Asteroid : PoolableUnit, IShootable
	{
		[SerializeField] private int _subAsteroids;
		[SerializeField] private Vector2 _speedRange;
		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private List<Sprite> _spriteList;
		[SerializeField] private Vector2 _sizeRange;

		public int SubAsteroids => _subAsteroids;
		public Vector3 Velocity => _velocity;
		public event Action<Asteroid> OnDisable;

		private float _size;
		protected Vector3 _velocity;
		private Vector2 _screenSize;

		private void Update()
		{
			Move();
			CheckOutOfScreen();
		}

		public void Init(Vector2 screenSize)
		{
			gameObject.SetActive(true);

			_screenSize = screenSize;

			SetSize();
			SetPosition();
			SetSpeed();

			SetSprite();
			SetSpriteRotation();
		}

		public void OnShotted()
		{
			DestroyAsteroid();
		}

		private void Move()
		{
			transform.position += _velocity * Time.deltaTime;
		}

		private void CheckOutOfScreen()
		{
			if (transform.position.x > _screenSize.x / 2 + _size
				|| transform.position.x < -_screenSize.x / 2 - _size
				|| transform.position.y > _screenSize.y / 2 + _size
				|| transform.position.y < -_screenSize.y / 2 - _size)
			{
				DestroyAsteroid();
			}
		}

		private void DestroyAsteroid()
		{
			OnDisable?.Invoke(this);
			gameObject.SetActive(false);
		}

		private void SetSize()
		{
			_size = Random.Range(_sizeRange.x, _sizeRange.y);
			transform.localScale = Vector3.one * _size;
		}

		private void SetPosition()
		{
			Vector2 position = new Vector2(GetPos(_screenSize.x), GetPos(_screenSize.y));
			transform.position = position;

			float GetPos(float value) => (value / 2 + _size) * Extensions.RandomSign();
		}

		private void SetSpeed()
		{
			float xSign = -Mathf.Sign(transform.position.x);
			float ySign = -Mathf.Sign(transform.position.y);

			Vector2 direction = new Vector3(Random.value * xSign, Random.value * ySign, 0).normalized;
			float speed = Random.Range(_speedRange.x, _speedRange.y);

			_velocity = direction * speed;
		}

		private void SetSpriteRotation()
		{
			float randomAngle = Random.Range(0, 360);
			_spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, randomAngle);
		}

		private void SetSprite()
		{
			int randomIndex = Random.Range(0, _spriteList.Count);
			_spriteRenderer.sprite = _spriteList[randomIndex];
		}
	}
}
