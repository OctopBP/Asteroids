using UnityEngine;

namespace Asteroids.Game
{
	public class SpaceshipEngene : MonoBehaviour
	{
		[Header("Fire")]
		[SerializeField] private Transform _fire;
		[SerializeField] private float _fireNoisePower = 0.1f;
		[SerializeField] private float _fireReaction = 100;

		private Vector3 _speed = Vector3.zero;
		private SpaceshipSettings _settings;
		private Transform _transform;
		private Vector3 _fireStartScale;

		private void Awake()
		{
			_fireStartScale = _fire.localScale;
		}

		public void Init(Transform spaceshipTransform, SpaceshipSettings settings)
		{
			_settings = settings;
			_transform = spaceshipTransform;
		}

		public void Move(bool moving)
		{
			ChangeSpeed(moving);
			ScaleFire(moving);
			Move();
		}

		private void Move()
		{
			_transform.position += _speed * Time.deltaTime;
		}

		private void ScaleFire(bool moving)
		{
			Vector3 targetScale = moving ? _fireStartScale * (1 + Random.Range(0, _fireNoisePower)) : Vector3.zero;
			_fire.localScale = Vector3.Lerp(_fire.localScale, targetScale, Time.deltaTime * _fireReaction);
		}

		public void ClampPosition(Vector2 screenSize)
		{
			_transform.position = new Vector3(
				x: ModAndOffcet(_transform.position.x, screenSize.x),
				y: ModAndOffcet(_transform.position.y, screenSize.y),
				z: _transform.position.z
			);

			float ModAndOffcet(float pos, float value) => (pos + value * 1.5f) % value - value / 2;
		}

		public void Turn(float turn)
		{
			float angle = _settings.TurnSpeed * turn * Time.deltaTime;
			_transform.rotation *= Quaternion.AngleAxis(angle, Vector3.back);
		}

		private void ChangeSpeed(bool moving)
		{
			if (moving)
				SpeedUp();
			else
				Drag();
		}

		private void SpeedUp()
		{
			_speed += _transform.up * _settings.Acceleration * Time.deltaTime;
			_speed = Mathf.Clamp(_speed.magnitude, 0, _settings.MaxSpeed) * _speed.normalized;
		}

		private void Drag()
		{
			float magnitude = _speed.magnitude;
			magnitude -= _settings.Drag * Time.deltaTime;
			_speed = magnitude * _speed.normalized;
		}
	}
}
