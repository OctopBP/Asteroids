using Asteroids.Input;

namespace Asteroids.Core
{
	public interface IState
	{
		void Init(Game game, IInput input);
		void Tick();
	}
}