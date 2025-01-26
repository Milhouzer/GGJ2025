using CaptainNemo.Controls;
using CaptainNemo.DotTweenTentacules;
using CaptainNemo.Game;
using DG.Tweening;
using UnityEngine;

public class ShakeController : ShakeComponent
{
	[SerializeField] private ControlHandler controlerHandler;
	[SerializeField] private float attenuation = 1;
	[SerializeField] private bool dontStart = false;

	private void Start()
	{
		if (!dontStart)
			Shake();
	}

	public override void Shake()
	{
		float controlValue = 0;

		switch (controlerHandler.GetGlobalControlParam())
		{
			case GlobalControlParam.Pressure:
				controlValue = GameManager.GetPressure();
				break;
			case GlobalControlParam.Temperature: 
				controlValue = GameManager.GetTemperature();
				break;
			case GlobalControlParam.Oxygen:
				controlValue = GameManager.GetOxygen();
				break;
		}
		
		shakeStrength = controlValue / attenuation;
		base.Shake();
	}

	public void StopShaking()
	{
		tweenShake.Kill();
	}

	protected override void OnCompleteTween()
	{
		base.OnCompleteTween();
		Shake();
	}
}
