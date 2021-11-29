using Asteroids.Data;
using Asteroids.Utils;
using UnityEngine;

namespace Asteroids.Game
{
	public class UnitFactory
	{
		private Vector2 _screenSize;
		private GameData _gameData;

		public UnitFactory(Vector2 screenSize, GameData gameData)
		{
			_screenSize = screenSize;
			_gameData = gameData;
		}

		public void SetupItem(PoolableUnit item)
		{
			float size = item.Size;
			item.transform.localScale = GetSize(size);
			item.transform.position = GetPosition(size);
		}

		private Vector3 GetPosition(float size)
		{
			return new Vector3(GetPos(_screenSize.x), GetPos(_screenSize.y));

			float GetPos(float value) => (value / 2 + size) * Extensions.RandomSign();
		}

		private Vector3 GetSize(float size)
		{
			return Vector3.one * size;
		}
	}
}