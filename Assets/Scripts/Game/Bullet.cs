using UnityEngine;

namespace Asteroids.Game
{
	public class Bullet : MonoBehaviour
	{
		private void Update()
		{
			transform.position += transform.up * Time.deltaTime * 10;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.TryGetComponent<IShootable>(out IShootable shootable)) return;
			shootable.OnShotted();
		}
	}
}