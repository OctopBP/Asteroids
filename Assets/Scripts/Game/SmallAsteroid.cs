using System;
using Asteroids.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Game
{
	public class SmallAsteroid : Asteroid
	{
		[Header("Small Asteroid")]
		[SerializeField] private Vector2 _randomRange;
		[SerializeField] private float _speedMultiplier = 2;

		public void Init(Vector2 screenSize, Vector3 position, Vector3 velocity)
		{
			base.Init(screenSize);

			SetPosition(position);
			SetSpeed(velocity);
		}

		private void SetSpeed(Vector3 velocity)
		{
			float speed = velocity.magnitude;
			float random = Random.Range(_randomRange.x, _randomRange.y) * Extensions.RandomSign();
			Vector3 cross = Vector3.Cross(velocity, Vector3.forward).normalized;
			_velocity = (velocity.normalized + cross * random).normalized * speed * _speedMultiplier;
		}

		private void SetPosition(Vector3 position)
		{
			transform.position = position;
		}
	}
}
