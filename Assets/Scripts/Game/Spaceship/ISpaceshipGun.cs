using Asteroids.Data;

namespace Asteroids.Game
{
	interface ISpaceshipGun
	{
		void Init(GameData gameData);
		void Fire();
	}
}
