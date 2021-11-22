using Asteroids.Data;
using TMPro;
using UnityEngine;

namespace Asteroids.Game.UI
{
	public class GameUI : MonoBehaviour
	{
		[SerializeField] private TMP_Text _scoreText;
		[SerializeField] private GameObject _loseUI;
		[SerializeField] private TMP_Text _resultText;
		[SerializeField] private string _resultFormat = "Result: {0}";

		private GameData _gameData;

		public void Init(GameData gameData)
		{
			_gameData = gameData;
			_gameData.OnScoreChange += ChangeScore;

			_loseUI.gameObject.SetActive(false);

			ChangeScore(_gameData.Score);
		}

		public void ShowResultScreen()
		{
			_resultText.SetText(_resultFormat, _gameData.Score);
			_loseUI.gameObject.SetActive(true);
		}

		private void ChangeScore(int score) => _scoreText.SetText($"{score}");
	}
}
