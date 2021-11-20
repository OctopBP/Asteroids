using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Game
{
	public class Asteroid : MonoBehaviour
	{
		[SerializeField] private Vector2 _speedRange;

		private float _size;
		private Vector3 _velocity;
		private Vector2 _screenSize;

		public event Action<Asteroid> OnDisable;

		private void Update()
		{
			Move();
			CheckOutOfScreen();
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
				OnDisable?.Invoke(this);
				gameObject.SetActive(false);
			}
		}

		public void Init(Vector2 screenSize)
		{
			gameObject.SetActive(true);

			_screenSize = screenSize;

			SetSize();
			SetPosition();
			SetSpeed();
		}

		private void SetSize()
		{
			_size = Random.Range(1f, 2f);
			transform.localScale = Vector3.one * _size;
		}

		private void SetPosition()
		{
			Vector2 position = new Vector2(GetPos(_screenSize.x), GetPos(_screenSize.y));
			transform.position = position;

			int RandomSign() => Random.value < 0.5f ? -1 : 1;
			float GetPos(float value) => (value / 2 + _size) * RandomSign();
		}

		private void SetSpeed()
		{
			float xSign = -Mathf.Sign(transform.position.x);
			float ySign = -Mathf.Sign(transform.position.y);

			Vector2 direction = new Vector3(Random.value * xSign, Random.value * ySign, 0).normalized;
			float speed = Random.Range(_speedRange.x, _speedRange.y);

			_velocity = direction * speed;
		}
	}
}
