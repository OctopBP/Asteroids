using UnityEngine;

namespace Asteroids.Game
{
	public class Spaceship : MonoBehaviour
	{
		[SerializeField] private float _maxSpeed = 5;
		[SerializeField] private float _acceleration = 0.1f;
		[SerializeField] private float _turnSpeed = 150;

		private float _speed = 0;

		public void Move(bool moving)
		{
			ChangeSpeed(moving);
			transform.position += transform.up * _speed * Time.deltaTime;
		}

		public void ClampPosition(Vector2 screenSize)
		{
			transform.position = new Vector3(
				x: ModAndOffcet(transform.position.x, screenSize.x),
				y: ModAndOffcet(transform.position.y, screenSize.y),
				z: transform.position.z
			);

			float ModAndOffcet(float pos, float value) => (pos + value * 1.5f) % value - value / 2;
		}

		public void Turn(float turn)
		{
			float angle = _turnSpeed * turn * Time.deltaTime;
			transform.rotation *= Quaternion.AngleAxis(angle, Vector3.back);
		}

		private void ChangeSpeed(bool moving)
		{
			float acceleration = _acceleration * (moving ? 1 : -1);
			_speed += acceleration * Time.deltaTime;
			_speed = Mathf.Clamp(_speed, 0, _maxSpeed);
		}
	}
}
