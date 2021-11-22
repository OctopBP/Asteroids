using Asteroids.Data;
using TMPro;
using UnityEngine;

namespace Asteroids.Game.UI
{
	public class GameUI : MonoBehaviour
	{
		[SerializeField] private TMP_Text _scoreText;
		[SerializeField] private TMP_Text _gameOverText;
		[SerializeField] private TMP_Text _resultText;

		private GameData _gameData;

		public void Init(GameData gameData)
		{
			_gameData = gameData;
			_gameData.OnScoreChange += ChangeScore;

			ChangeScore(_gameData.Score);
		}

		private void ChangeScore(int score) => _scoreText.SetText($"{score}");
	}
}
