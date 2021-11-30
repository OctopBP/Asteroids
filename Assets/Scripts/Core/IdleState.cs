using Asteroids.Input;
using UnityEngine.SceneManagement;

namespace Asteroids.Core
{
	public class IdleState : IState
	{
		public void Init(Game game, IInput input)
		{
			SceneManager.LoadScene(1, LoadSceneMode.Additive);
		}
		
		public void Tick() { }
	}
}