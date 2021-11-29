using Asteroids.Data;
using Asteroids.Utils;
using UnityEngine;

namespace Asteroids.Game
{
	public class SmallAsteroid : Asteroid
	{
		[Header("Small Asteroid")] [SerializeField]
		private Vector2 _randomRange;

		[SerializeField] private float _speedMultiplier = 2;

		public void Init(GameData gameData, Vector3 position, Vector3 velocity)
		{
			base.Init(gameData);

			SetPosition(position);
			SetSpeed(velocity);
		}

		private void SetSpeed(Vector3 velocity)
		{
			float speed = velocity.magnitude;
			float random = Random.Range(_randomRange.x, _randomRange.y) * Extensions.RandomSign();
			Vector3 cross = Vector3.Cross(velocity, Vector3.forward).normalized;
			Velocity = (velocity.normalized + cross * random).normalized * speed * _speedMultiplier;
		}

		private void SetPosition(Vector3 position)
		{
			transform.position = position;
		}
	}
}