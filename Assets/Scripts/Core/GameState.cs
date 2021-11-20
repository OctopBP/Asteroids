using Asteroids.Game;
using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Core
{
	public class GameState : MonoBehaviour, IState
	{
		[SerializeField] private Spaceship _spaceship;

		private IInput _input;

		public void Init(IInput input)
		{
			_input = input;
		}

		public void Tick()
		{
			HandleMove();
			HandleTurn();
		}

		private void HandleMove()
		{
			if (_input.IsMoving)
				_spaceship.Move();
		}

		private void HandleTurn()
		{
			if (_input.Turn != 0)
				_spaceship.Turn(_input.Turn);
		}
	}
}
