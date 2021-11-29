using UnityEngine;

namespace Asteroids.Data
{
	public class SpaceshipData
	{
		public int BulletsCount { get; private set; }
		public Vector2 Coords { get; private set; }
		public float Angle { get; private set; }
		public float Speed { get; private set; }

		private void SetDate(int bulletsCount, Vector2 coords, float angle, float speed) { }
	}
}