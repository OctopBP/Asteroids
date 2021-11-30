using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Core
{
	public class Game : MonoBehaviour
	{
		[SerializeField] private MenuState _menuState;

		private IInput _input;
		private IState _state;

		private void Start()
		{
			_input = new UnityInput();
			NewState(_menuState);
		}

		private void Update()
		{
			_state.Tick();
		}

		public void StartGame() => NewState(new IdleState());

		private void NewState(IState state)
		{
			_state = state;
			_state.Init(this, _input);
		}
	}
}