using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asteroids.Data;
using Asteroids.Utils;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using Random = UnityEngine.Random;

namespace Asteroids.Game
{
	public class Asteroid : PoolableUnit, IShootable
	{
		[SerializeField] private int _subAsteroids;
		[SerializeField] private Vector2 _speedRange;
		[SerializeField] private Vector2 _sizeRange;
		[Space]
		[SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private List<Sprite> _spriteList;
		[SerializeField] private Color _color1 = Color.white;
		[SerializeField] private Color _color2 = Color.white;
		[Space]
		[SerializeField] private ParticleSystem _expolosionPS;

		public int SubAsteroids => _subAsteroids;
		public Vector3 Velocity => _velocity;
		public event Action<Asteroid> OnDisable;

		private float _size;
		protected Vector3 _velocity;

		private GameData _gameData;
		private Vector2 _screenSize;

		private void Update()
		{
			Move();
			CheckOutOfScreen();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.TryGetComponent<ICollisionRactive>(out ICollisionRactive colidable)) return;
			colidable.OnCollision();
		}

		public void Init(Vector2 screenSize, GameData gameData)
		{
			gameObject.SetActive(true);

			_gameData = gameData;
			_screenSize = screenSize;

			SetSize();
			SetPosition();
			SetSpeed();

			SetSprite();
			SetSpriteRotation();
		}

		public void OnShotted()
		{
			SpawnPS();
			AddScore();
			DestroyAsteroid();
		}

		private void AddScore()
		{
			_gameData.AddScore();
		}

		private async void SpawnPS()
		{
			ParticleSystem newPS = Instantiate(_expolosionPS, transform.position, Quaternion.identity);

			MainModule main = newPS.main;
			main.startColor = _spriteRenderer.color;

			await Task.Delay(Mathf.RoundToInt(newPS.main.startLifetime.constantMax * 1000));
			Destroy(newPS.gameObject);
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
			_spriteRenderer.color = Color.Lerp(_color1, _color2, Random.value);
		}
	}
}
