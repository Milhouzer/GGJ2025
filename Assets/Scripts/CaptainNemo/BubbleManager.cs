using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    //For test
    public Bubble bubble = default;

    public AnimationCurve minHeatSpeedCurve = default;
    public AnimationCurve maxHeatSpeedCurve = default;

    public AnimationCurve mouthStrengthByTime = default;
    //

    public static float fieldRay { get; } = 4.5f;

    private void Awake()
    {
        bubble.CurrentMouthStrength = mouthStrengthByTime.Evaluate(Time.time / 10f);
        bubble.CurrentRandomSpeed = 1f;// = new Vector2(minHeatSpeedCurve.Evaluate(Time.time / 10f), maxHeatSpeedCurve.Evaluate(Time.time / 10f)) * 10;
        bubble.CurrentRandomRotationRange = new Vector2(-30, 30);// = Random.Range(minHeatSpeedCurve.Evaluate(Time.time / 10f), maxHeatSpeedCurve.Evaluate(Time.time / 10f)) * 10;
        bubble.CurrentTimeBetweenAddForce = 0.05f;// minHeatSpeedCurve.Evaluate(Time.time / 10f);
        bubble.Init(5);
    }

    private void Update()
    {
        //bubble.CurrentMouthStrength = mouthStrengthByTime.Evaluate(Time.time / 10f);
        //bubble.CurrentRandomSpeedRange = new Vector2(minHeatSpeedCurve.Evaluate(Time.time / 10f), maxHeatSpeedCurve.Evaluate(Time.time / 10f)) * 10;
        //bubble.CurrentRandomRotationSpeed = Random.Range(minHeatSpeedCurve.Evaluate(Time.time / 10f), maxHeatSpeedCurve.Evaluate(Time.time / 10f)) * 10;
        //bubble.CurrentTimeBetweenAddForce = minHeatSpeedCurve.Evaluate(Time.time / 10f);
    }
    //
}
