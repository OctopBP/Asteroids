using System;
using UnityEngine;

namespace Asteroids.Data
{
	public class SpaceshipData
	{
		public Vector2 Coords { get; private set; }
		public float Angle { get; private set; }
		public float Speed { get; private set; }
		
		public event Action<int> OnBulletsCountChange;

		public void SetBulletCount(int bulletsCount)
		{
			OnBulletsCountChange?.Invoke(bulletsCount);
		}

		public void SetCoords(Vector2 coords, float angle)
		{
			Coords = coords;
			Angle = angle;
		}

		public void SetSpeed(float speed)
		{
			Speed = speed;
		}
	}
}