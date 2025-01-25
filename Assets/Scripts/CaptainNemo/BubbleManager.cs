using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public Bubble bubble = default;

    private bool currentCalm = true;
    [SerializeField] private bool newCalm = true;

    public static float fieldRay { get; } = 4.5f;

    private void Awake()
    {
        bubble.Init();
    }

    private void Update()
    {
        if (currentCalm != newCalm)
        {
            if (newCalm)
            {
                bubble.CurrentTimeBetweenChangeTargetDirection = 0.5f;
                bubble.CurrentRotationSpeed = 0.03f;
                bubble.CurrentTargetRotationChangeRange = new Vector2(-30, 30);
                bubble.CurrentSpeed = 0.5f;
            }
            else
            {
                bubble.CurrentTimeBetweenChangeTargetDirection = 0.1f;
                bubble.CurrentRotationSpeed = 1f;
                bubble.CurrentTargetRotationChangeRange = new Vector2(-180, 180);
                bubble.CurrentSpeed = 2f;
            }

            currentCalm = newCalm;
        }
    }
}
