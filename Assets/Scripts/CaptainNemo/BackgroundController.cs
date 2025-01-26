using UnityEngine;

public class BackgroundController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer = default;
	[SerializeField] private Color endBackgroundColor = default;
	[SerializeField] private float maxTime = 180;

	private Color startColor = default;
	private float currentTime = default;

	private void Start()
	{
		startColor = spriteRenderer.color;	
	}

	private void Update()
	{
		currentTime += Time.deltaTime;
		float ratio = currentTime / maxTime;

		Color color = Color.Lerp(startColor,endBackgroundColor,ratio);
		spriteRenderer.color = color;
	}
}
