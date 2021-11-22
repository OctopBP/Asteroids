using Asteroids.Input;

namespace Asteroids.Game
{
	public interface IGameState
	{
		void Init(GameLoop game, IInput input);
		void Tick();
	}
}
