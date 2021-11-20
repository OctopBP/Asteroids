using UnityEngine;

namespace Asteroids.Game
{
	public class Spaceship : MonoBehaviour
	{
		private const float Speed = 5;
		private const float TurnSpeed = 150;

		public void Move()
		{
			transform.position += transform.up * Time.deltaTime * Speed;
		}

		public void Turn(float turn)
		{
			transform.rotation *= Quaternion.AngleAxis(TurnSpeed * turn * Time.deltaTime, Vector3.back);
		}
	}
}
