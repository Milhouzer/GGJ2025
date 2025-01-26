using CaptainNemo.Controls;
using CaptainNemo.DotTweenTentacules;
using CaptainNemo.Game;
using UnityEngine;

public class RotateThermostat : MonoBehaviour
{
	[SerializeField] private float maxTemperature = 0f;
	[SerializeField] private Transform transformThermostat = default;
	[SerializeField] private ShakeController shakeController;
	[SerializeField] private float maxAngle = 300f;

	private float startAngle = 0;
	private bool startShaking = false;

	private void Start()
	{
		startAngle = transformThermostat.rotation.z;
	}

	private void Update()
	{
		float ratio = GameManager.GetTemperature() / maxTemperature;
		ratio = Mathf.Clamp(ratio, 0f, 1f);
		Quaternion currentAngle = Quaternion.AngleAxis(startAngle + ratio * maxAngle * -1,Vector3.forward);

		transform.rotation = currentAngle;

		if(ratio >= 1 && !startShaking)
		{
			startShaking = true;
			shakeController.Shake();
		}else if(ratio < 1)
			startShaking=false;
	}
}
