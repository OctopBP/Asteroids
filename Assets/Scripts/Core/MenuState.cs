using System.Collections.Generic;
using Asteroids.Input;
using UnityEngine;

namespace Asteroids.Core
{
	public class MenuState : MonoBehaviour, IState
	{
		[SerializeField] private List<GameObject> _toEnable;
		[SerializeField] private List<GameObject> _toDisable;

		private Game _game;
		private IInput _input;

		public static IState Instance { get; private set; }

		private void Awake()
		{
			Instance = Instance ?? this;
		}

		public void Init(Game game, IInput input)
		{
			_game = game;
			_input = input;
			_input.Start += Play;
		}

		public void Tick() { }

		private void Play()
		{
			_input.Start -= Play;

			foreach (GameObject go in _toEnable)
			{
				go.SetActive(true);
			}

			foreach (GameObject go in _toDisable)
			{
				go.SetActive(false);
			}

			_game.StartGame();
		}
	}
}
