using UnityEngine;

namespace Asteroids.Game
{
	[CreateAssetMenu(fileName = "SpaceshipSettings", menuName = "Settings/Spaceship", order = 0)]
	public class SpaceshipSettings : ScriptableObject
	{
		[SerializeField] private float _maxSpeed = 5;
		[SerializeField] private float _acceleration = 5;
		[SerializeField] private float _drag = 2;
		[SerializeField] private float _turnSpeed = 150;

		public float MaxSpeed => _maxSpeed;
		public float Acceleration => _acceleration;
		public float Drag => _drag;
		public float TurnSpeed => _turnSpeed;
	}
}
