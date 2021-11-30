using System;

namespace Asteroids.Input
{
	public interface IInput
	{
		bool IsMoving { get; }
		float Turn { get; }
		bool IsFire { get; }
		bool IsLaser { get; }
		event Action Start;
	}
}