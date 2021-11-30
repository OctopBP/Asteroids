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
		[SerializeField] private UfoFactory _ufoFactory;
		[SerializeField] private GameUI _gameUI;

		private GameLoop _game;
		private GameData _gameData;
		private IInput _input;
		private Vector2 _screenSize;
		private UnitFactory _unitFactory;

		public void Init(GameLoop game, IInput input)
		{
			_game = game;
			_input = input;

			_gameData = new GameData();
			_screenSize = CalculateCameraSize();

			_unitFactory = new UnitFactory(_screenSize, _gameData);

			_spaceship.Init(this, _gameData);
			_gameUI.Init(_gameData);
			_asteroidFactory.Init(_gameData, _unitFactory);
			_ufoFactory.Init(_gameData, _unitFactory, _spaceship);
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
			HandleLaser();

			UpdateUI();
			TickFactorys();
		}

		private void UpdateUI()
		{
			_gameUI.Tick();
		}

		private void TickFactorys()
		{
			_asteroidFactory.Tick();
			_ufoFactory.Tick();
		}

		private Vector2 CalculateCameraSize()
		{
			return new Vector2(Screen.width, Screen.height) * 2 / 100;
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

		private void HandleLaser()
		{
			_spaceship.Laser(_input.IsLaser);
		}
	}
}