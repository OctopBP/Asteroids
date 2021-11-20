using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Core
{
	public class Game : MonoBehaviour
	{
		[SerializeField] private GameState _gameState;

		private IInput _input;
		private IState _state;

		private void Start()
		{
			_input = new UnityInput();
			NewState(_gameState);
		}

		private void Update()
		{
			_state.Tick();
		}

		private void NewState(GameState state)
		{
			_state = state;
			_state.Init(_input);
		}
	}
}
