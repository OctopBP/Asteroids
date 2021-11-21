using UnityEngine;

namespace Asteroids.Utils
{
	public class RotateObject : MonoBehaviour
	{
		[SerializeField] private Vector3 _speed;

		private void Update()
		{
			transform.rotation *= Quaternion.Euler(_speed * Time.deltaTime);
		}
	}
}
