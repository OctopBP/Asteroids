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
		[SerializeField] private string _speedFormat = "Speed {0:D2}";
		
		[Space(10), SerializeField] private TMP_Text _coordsText;
		[SerializeField] private string _coordsFormat = "X {0:D2} {1:D2}";
		
		[Space(10), SerializeField] private TMP_Text _angleText;
		[SerializeField] private string _angleFormat = "A {0:D2}";
		
		[Space(10), SerializeField] private TMP_Text _bulletsText;
		[SerializeField] private string _bulletsFormat = "{0}";

		private GameData _gameData;

		public void Init(GameData gameData)
		{
			_gameData = gameData;
			_gameData.OnScoreChange += ChangeScore;
			_gameData.SpaceshipData.OnBulletsCountChange += ChangeBulletsCount;

			_loseUI.gameObject.SetActive(false);

			ChangeScore(_gameData.Score);
		}

		public void Tick()
		{
			SetSpeed();
			SetCoords();
			SetAngle();
		}

		private void SetCoords()
		{
			Vector2 coords = _gameData.SpaceshipData.Coords;
			_coordsText.SetText(_coordsFormat, coords.x, coords.y);
		}

		private void SetAngle()
		{
			_angleText.SetText(_angleFormat, _gameData.SpaceshipData.Angle);
		}

		private void SetSpeed()
		{
			_speedText.SetText(_speedFormat, _gameData.SpaceshipData.Speed);
		}

		public void ShowResultScreen()
		{
			_resultText.SetText(_resultFormat, _gameData.Score);
			_loseUI.gameObject.SetActive(true);
		}

		private void ChangeScore(int score) => _scoreText.SetText($"{score}");
		private void ChangeBulletsCount(int count) => _bulletsText.SetText(_bulletsFormat, count);
	}
}