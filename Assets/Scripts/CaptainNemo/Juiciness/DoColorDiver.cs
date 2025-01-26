using CaptainNemo.Game;
using UnityEngine;

public class DoColorDiver : MonoBehaviour
{
    [SerializeField] private Color colorToAchieve;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Color startColor = default;

	private void Start()
	{
		startColor = spriteRenderer.color;
	}

	void Update()
    {
        float ratio = GameManager.GetOxygen() / GameManager.GetMaxOxygenValue();
        spriteRenderer.color = Color.Lerp(startColor, colorToAchieve,ratio);       
    }
}
