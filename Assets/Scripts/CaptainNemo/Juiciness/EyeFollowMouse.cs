using UnityEngine;

public class EyeFollowMouse : MonoBehaviour
{
	[SerializeField] private float force = 0.01f;
	[SerializeField] private Vector2 clampDistance = Vector2.zero;
	[SerializeField] private Vector3 offsset;

	private Vector3 startPosition = Vector3.zero;

	private void Start()
	{
		startPosition = transform.position;
	}

	private void Update()
	{
		Vector3 screenMousePosition = new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y,
			Camera.main.transform.position.z * -1);
		Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(screenMousePosition);

		Vector3 direction = worldMousePosition - startPosition;
		float distance =  Mathf.Clamp(direction.magnitude,clampDistance.x,clampDistance.y);
		direction = direction.normalized;

		Debug.DrawRay(startPosition, direction * distance * force, Color.red,100f);

		transform.position = startPosition + offsset + direction * distance * force;
	}
}
