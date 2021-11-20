using UnityEngine.InputSystem;

namespace Asteroids.Input
{
	public class UnityInput : IInput
	{
		private InputControls _inputControls;

		public bool IsMoving { get; private set; }
		public float Turn { get; private set; }

		public UnityInput()
		{
			_inputControls = new InputControls();

			_inputControls.ActionMap.Move.performed += MovePerformed;
			_inputControls.ActionMap.Move.canceled += MoveCanceled;

			_inputControls.ActionMap.Turn.performed += TurnPerformed;
			_inputControls.ActionMap.Turn.canceled += TurnCanceled;

			_inputControls.Enable();
		}

		private void MovePerformed(InputAction.CallbackContext ctx)
		{
			IsMoving = true;
		}

		private void MoveCanceled(InputAction.CallbackContext ctx)
		{
			IsMoving = false;
		}

		private void TurnPerformed(InputAction.CallbackContext ctx)
		{
			float turn = ctx.ReadValue<float>();
			Turn = turn;
		}

		private void TurnCanceled(InputAction.CallbackContext ctx)
		{
			Turn = 0;
		}
	}
}
