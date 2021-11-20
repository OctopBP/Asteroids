using Asteroids.Game;
using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Core
{
	public class GameState : MonoBehaviour, IState
	{
		[SerializeField] private Camera _camera;
		[SerializeField] private Spaceship _spaceship;

		private IInput _input;
		private Vector2 _screenSize;

		public void Init(IInput input)
		{
			_input = input;
			CalculateCameraSize();
		}

		public void Tick()
		{
			HandleMove();
			HandleTurn();
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
	}
}
