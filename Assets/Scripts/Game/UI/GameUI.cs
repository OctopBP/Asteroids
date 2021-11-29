using Asteroids.Data;
using TMPro;
using UnityEngine;

namespace Asteroids.Game.UI
{
	public class GameUI : MonoBehaviour
	{
		[SerializeField] private TMP_Text _scoreText;
		[SerializeField] private GameObject _loseUI;
		[Space(10), SerializeField] private TMP_Text _resultText;
		[SerializeField] private string _resultFormat = "Result: {0}";
		[Space(10), SerializeField] private TMP_Text _speedText;
		[SerializeField] private string _speedFormat = "Speed {0}";
		[Space(10), SerializeField] private TMP_Text _coordsText;
		[SerializeField] private string _coordsFormat = "X {0} {1}";
		[Space(10), SerializeField] private TMP_Text _angleText;
		[SerializeField] private string _angleFormat = "A {0}";
		[Space(10), SerializeField] private TMP_Text _bulletsText;
		[SerializeField] private string _bulletsFormat = "{0}";

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