using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Core
{
	public class Game : MonoBehaviour
	{
		[SerializeField] private bool _loadGame;

		private IInput _input;
		private IState _state;

		private void Start()
		{
			// _input = new UnityInput();
			// NewState(_loadGame ? GameState.Instance : MenuState.Instance);
		}

		// private void Update()
		// {
		// 	_state.Tick();
		// }

		// public void StartGame() => NewState(GameState.Instance);
		// public void OpenMenu() => NewState(MenuState.Instance);

		// private void NewState(IState state)
		// {
		// 	_state = state;
		// 	_state.Init(this, _input);
		// }
	}
}
