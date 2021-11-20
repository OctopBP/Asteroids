using Asteroids.Input;

namespace Asteroids.Core
{
	public interface IState
	{
		void Init(IInput input);
		void Tick();
	}
}