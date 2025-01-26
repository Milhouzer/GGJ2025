using CaptainNemo.Controls;
using CaptainNemo.DotTweenTentacules;
using DG.Tweening;
using UnityEngine;

public class ShakeController : ShakeComponent
{
	[SerializeField] private ControlHandler controlerHandler;
	[SerializeField] private float attenuation = 1;

	private void Start()
	{
		Shake();
	}

	public override void Shake()
	{
		shakeStrength = controlerHandler.GetControlValue() / attenuation;
		base.Shake();

		Debug.LogWarning("Shake strength" + shakeStrength);
	}

	protected override void OnCompleteTween()
	{
		base.OnCompleteTween();
		Shake();
	}
}
