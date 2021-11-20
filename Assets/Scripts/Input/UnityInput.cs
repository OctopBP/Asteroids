using UnityEngine.InputSystem;

namespace Asteroids.Input
{
	public class UnityInput : IInput
	{
		private InputControls _inputControls;

		public bool IsMoving { get; private set; }
		public float Turn { get; private set; }
		public bool IsFire { get; private set; }

		public UnityInput()
		{
			_inputControls = new InputControls();

			_inputControls.ActionMap.Move.performed += MovePerformed;
			_inputControls.ActionMap.Move.canceled += MoveCanceled;

			_inputControls.ActionMap.Turn.performed += TurnPerformed;
			_inputControls.ActionMap.Turn.canceled += TurnCanceled;

			_inputControls.ActionMap.Fire.performed += FirePerformed;
			_inputControls.ActionMap.Fire.canceled += FireCanceled;

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

		private void FirePerformed(InputAction.CallbackContext ctx)
		{
			IsFire = true;
		}

		private void FireCanceled(InputAction.CallbackContext ctx)
		{
			IsFire = false;
		}
	}
}
