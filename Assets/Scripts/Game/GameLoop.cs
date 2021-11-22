using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Game
{
	public class GameLoop : MonoBehaviour
	{
		[SerializeField] private GameState _gameState;

		private IInput _input;
		private IGameState _currentState;
		private IGameState _waitState;

		private void Start()
		{
			Init();
		}

		private void Init()
		{
			_input = new UnityInput();
			_waitState = new WaitState();

			StartGame();
		}

		private void SetState(IGameState state)
		{
			_currentState = state;
			_currentState.Init(this, _input);
		}

		public void StartGame()
		{
			SetState(_gameState);
		}

		public void Lose()
		{
			SetState(_waitState);
		}

		private void Update()
		{
			_currentState.Tick();
		}
	}
}
