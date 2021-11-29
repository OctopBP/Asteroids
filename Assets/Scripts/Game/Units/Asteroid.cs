using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asteroids.Data;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using Random = UnityEngine.Random;

namespace Asteroids.Game
{
	public class Asteroid : PoolableUnit, IShootable
	{
		[SerializeField] private int _subAsteroids;
		[SerializeField] private Vector2 _speedRange;
		[Space] [SerializeField] private SpriteRenderer _spriteRenderer;
		[SerializeField] private List<Sprite> _spriteList;
		[SerializeField] private Color _color1 = Color.white;
		[SerializeField] private Color _color2 = Color.white;
		[Space] [SerializeField] private ParticleSystem _expolosionPS;

		public int SubAsteroids => _subAsteroids;
		public event Action<Asteroid> OnDisable;

		private float _size;
		public Vector3 Velocity { get; protected set; }

		private GameData _gameData;
		private Vector2 _screenSize;

		private void Update()
		{
			Move();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.TryGetComponent(out ICollisionRactive collidable)) return;
			collidable.OnCollision(Velocity);
		}

		public void Init(GameData gameData)
		{
			gameObject.SetActive(true);

			_gameData = gameData;

			SetSpeed();

			SetSprite();
			SetSpriteRotation();
		}

		public void OnShot()
		{
			SpawnPS();
			AddScore();
			DestroyUnit();
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

			await Task.Delay(Mathf.RoundToInt(main.startLifetime.constantMax * 1000));
			Destroy(newPS.gameObject);
		}

		private void Move()
		{
			transform.position += Velocity * Time.deltaTime;
		}

		protected override void DestroyUnit()
		{
			if (!gameObject.activeSelf) return;

			OnDisable?.Invoke(this);
			gameObject.SetActive(false);
		}

		private void SetSpeed()
		{
			float xSign = -Mathf.Sign(transform.position.x);
			float ySign = -Mathf.Sign(transform.position.y);

			Vector2 direction = new Vector3(Random.value * xSign, Random.value * ySign, 0).normalized;
			float speed = Random.Range(_speedRange.x, _speedRange.y);

			Velocity = direction * speed;
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