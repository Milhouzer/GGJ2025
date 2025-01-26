using DG.Tweening;
using UnityEngine;

namespace CaptainNemo.DotTweenTentacules
{

	public class ShakeTentacules : MonoBehaviour
	{
		[SerializeField] private float shakeStrength = 1f;
		[SerializeField] private float duration = 1f;
		[SerializeField] private int vibrato = 1;
		[SerializeField] private SpriteRenderer sprite;
		[SerializeField] private Ease easeCurve;

		public void Shake()
		{
			sprite.transform.DOShakePosition(duration, shakeStrength, vibrato).SetEase(easeCurve);
		}
	}
}
