using UnityEngine;
using System.Collections;

namespace Asteroids.Game
{
	public class LaserRay : MonoBehaviour
	{
		[Header("Scale"), SerializeField] private float _scaleTime = 0.2f;
		[SerializeField] private float _scaleSpeed = 100f;

		[Header("Fly"), SerializeField] private float _flyTime = 0.1f;
		[SerializeField] private float _flySpeed = 50f;

		public void Release()
		{
			gameObject.SetActive(true);
			StartCoroutine(Scale());
		}
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.TryGetComponent<IShootable>(out IShootable shootable)) return;
			shootable.OnShot();
		}
		
		private IEnumerator Scale()
		{
			transform.localPosition = Vector3.zero;
			
			for (float t = 0f; t < _scaleTime; t += Time.deltaTime)
			{
				transform.localScale = new Vector3(transform.localScale.x, t * _scaleSpeed, transform.localScale.z);
				yield return null;
			}

			for (float t = 0f; t < _flyTime; t += Time.deltaTime)
			{
				transform.localPosition = new Vector3(0, t * _flySpeed, 0);
				yield return null;
			}

			gameObject.SetActive(false);
		}
	}
}