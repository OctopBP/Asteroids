using Asteroids.Data;
using UnityEngine;

namespace Asteroids.Game
{
	public class Spaceship : MonoBehaviour, ICollisionRactive
	{
		[SerializeField] private SpaceshipSettings _settings;
		[SerializeField] private SpaceshipEngene _spaceshipEngene;

		private ISpaceshipGun _spaceshipGun;
		private GameState _gameState;
		private GameData _gameData;

		private void Start()
		{
			_spaceshipGun = GetComponent<SpaceshipGun>();
		}

		public void Init(GameState gameState, GameData gameData)
		{
			_gameState = gameState;
			_gameData = gameData;

			transform.position = Vector3.zero;
			transform.rotation = Quaternion.identity;

			_spaceshipEngene.Init(transform, _settings);
		}

		public void Move(bool moving)
		{
			_spaceshipEngene.Move(moving);
		}

		public void ClampPosition(Vector2 screenSize)
		{
			_spaceshipEngene.ClampPosition(screenSize);
		}

		public void Turn(float turn)
		{
			_spaceshipEngene.Turn(turn);
		}

		public void Fire(bool isFire)
		{
			if (isFire) _spaceshipGun.Fire();
		}

		public void OnCollision()
		{
			_gameState.Lose();
		}
	}
}
