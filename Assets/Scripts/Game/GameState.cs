using Asteroids.Data;
using Asteroids.Game.UI;
using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Game
{
	public class GameState : MonoBehaviour, IGameState
	{
		[SerializeField] private Camera _camera;
		[SerializeField] private Spaceship _spaceship;
		[SerializeField] private AsteroidFactory _asteroidFactory;
		[SerializeField] private GameUI _gameUI;

		private GameLoop _game;
		private GameData _gameData;
		private IInput _input;
		private Vector2 _screenSize;

		public void Init(GameLoop game, IInput input)
		{
			_game = game;
			_input = input;

			_gameData = new GameData();

			CalculateCameraSize();

			_gameUI.Init(_gameData);
			_spaceship.Init(this, _gameData);
			_asteroidFactory.Init(_screenSize, _gameData);
		}

		public void Lose()
		{
			_gameUI.ShowResultScreen();
			_game.Lose();
		}

		public void Tick()
		{
			HandleMove();
			HandleTurn();
			HandleFire();
		}

		private void CalculateCameraSize()
		{
			float cameraSize = _camera.orthographicSize * 2;
			float aspect = _camera.aspect;
			_screenSize = new Vector2(cameraSize * aspect, cameraSize);
		}

		private void HandleMove()
		{
			_spaceship.Move(_input.IsMoving);
			_spaceship.ClampPosition(_screenSize);
		}

		private void HandleTurn()
		{
			_spaceship.Turn(_input.Turn);
		}

		private void HandleFire()
		{
			_spaceship.Fire(_input.IsFire);
		}
	}
}
