using Asteroids.Input;

namespace Asteroids.Game
{
	public class WaitState : IGameState
	{
		private GameLoop _game;
		private IInput _input;

		public void Init(GameLoop game, IInput input)
		{
			_game = game;
			_input = input;

			_input.Start += StartGame;
		}

		private void StartGame()
		{
			_input.Start -= StartGame;
			_game.StartGame();
		}

		public void Tick() { }
	}
}
