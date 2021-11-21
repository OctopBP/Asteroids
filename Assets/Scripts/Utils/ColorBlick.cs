using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Asteroids.Utils
{
	public class ColorBlick : MonoBehaviour
	{
		[SerializeField] private Color _color1;
		[SerializeField] private Color _color2;
		[SerializeField] private TMP_Text _text;
		[SerializeField] private float _time;
		[SerializeField] private AnimationCurve _curve;

		private void Start()
		{
			BlickLoop();
		}

		private async void BlickLoop()
		{
			while (Application.isPlaying) await Blick();
		}

		private async Task Blick()
		{
			await LerpColor(_color1, _color2);
			await LerpColor(_color2, _color1);
		}

		private async Task LerpColor(Color from, Color to)
		{
			for (float i = 0; i < _time; i += Time.deltaTime)
			{
				_text.color = Color.Lerp(from, to, _curve.Evaluate(i / _time));
				await Task.Yield();
			}
		}
	}
}