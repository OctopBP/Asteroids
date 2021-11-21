using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Core
{
	public class Game : MonoBehaviour
	{
		private IInput _input;
		private IState _state;

		private void Start()
		{
			_input = new UnityInput();
			NewState(MenuState.Instance);
		}

		private void Update()
		{
			_state.Tick();
		}

		public void StartGame() => NewState(GameState.Instance);

		private void NewState(IState state)
		{
			_state = state;
			_state.Init(this, _input);
		}
	}
}
