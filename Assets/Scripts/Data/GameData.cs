using System;

namespace Asteroids.Data
{
	public class GameData
	{
		public SpaceshipData SpaceshipData { get; private set; }

		public event Action<int> OnScoreChange;
		public int Score { get; private set; }

		public void AddScore() => SetScore(Score + 1);
		public void ResetScore() => SetScore(0);

		private void SetScore(int score)
		{
			Score = score;
			OnScoreChange?.Invoke(Score);
		}
	}

}