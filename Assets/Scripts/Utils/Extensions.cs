using UnityEngine;

namespace Asteroids.Utils
{
	public partial class Extensions
	{
		/// <summary>Случайно возвращает 1 или -1</summary>
		/// <param name="probability">Вероятность выпадения 1</param>
		public static int RandomSign(float probability = 0.5f)
		{
			return Random.value < probability ? 1 : -1;
		}
	}
}