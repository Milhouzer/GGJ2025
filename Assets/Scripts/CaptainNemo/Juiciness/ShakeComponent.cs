using DG.Tweening;
using System;
using UnityEngine;

namespace CaptainNemo.DotTweenTentacules
{

	public class ShakeComponent : MonoBehaviour
	{
		[SerializeField] protected float shakeStrength = 1f;
		[SerializeField] private float duration = 1f;
		[SerializeField] private int vibrato = 1;
		[SerializeField] private SpriteRenderer sprite;
		[SerializeField] private Ease easeCurve;
		[SerializeField] private bool loop = false;

		protected Tween tweenShake = default;

		virtual public void Shake()
		{
			tweenShake = sprite.transform.DOShakePosition(duration, shakeStrength, vibrato).SetEase(easeCurve);

			if (loop)
				tweenShake.Play().SetLoops(-1);
			else
				tweenShake.Play();

			tweenShake.OnComplete(OnCompleteTween);
		}

		virtual protected void OnCompleteTween()
		{
			
		}
	}
}
