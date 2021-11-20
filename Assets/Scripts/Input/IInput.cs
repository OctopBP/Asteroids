namespace Asteroids.Input
{
	public interface IInput
	{
		bool IsMoving { get; }
		float Turn { get; }
	}
}