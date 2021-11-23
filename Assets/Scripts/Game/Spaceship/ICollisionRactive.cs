using UnityEngine;

namespace Asteroids.Game
{
	public interface ICollisionRactive
	{
		void OnCollision(Vector3 from);
	}
}