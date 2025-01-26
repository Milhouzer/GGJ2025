using System.Collections.Generic;
using CaptainNemo.Bubbles;
using CaptainNemo.Player;
using UnityEngine;

namespace CaptainNemo
{
    public class TestManager : MonoBehaviour
    {
        // private bool currentCalm = true;
        // [SerializeField] private bool newCalm = true;
        // [SerializeField] private List<Bubble> bubbles = default;
        // [SerializeField] private Diver diver = default;
        //
        // [SerializeField] private AnimationCurve oxygenByPressure = default;
        // [SerializeField] private AnimationCurve temperatureByTime = default;
        //
        // [SerializeField] private Bubble.BubbleMovementRanges bubbleMovementRanges = default;
        //
        // [SerializeField] private float spawnChildrenDistanceFromCenter = 0.5f;
        //
        // private float elapsedTime = 0f;
        //
        // public static float pressure = 0f;
        //
        // private float maxOxygenForOneBubble = 100f;
        //
        // private void Update()
        // {
        //     Temperature();
        //
        //     Pressure();
        //
        //     if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
        //         pressure = 1f;
        //
        //     if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse1))
        //         pressure = 0f;
        //
        //     //TestDivideBubble();
        // }
        //
        // private void Temperature()
        // {
        //     elapsedTime += Time.deltaTime;
        //     float temperature = temperatureByTime.Evaluate(elapsedTime / 30f);// = diver.TemperatureLevel * 0.01f;
        //
        //     Bubble.CurrentTimeBetweenChangeTargetDirection = Mathf.Lerp(
        //         bubbleMovementRanges.timeBetweenChangeTargetDirection.x,
        //         bubbleMovementRanges.timeBetweenChangeTargetDirection.y,
        //         temperature);
        //
        //     Bubble.CurrentRotationSpeed = Mathf.Lerp(
        //         bubbleMovementRanges.rotationSpeed.x,
        //         bubbleMovementRanges.rotationSpeed.y,
        //         temperature);
        //
        //     Bubble.CurrentAmplitude = Mathf.Lerp(
        //         bubbleMovementRanges.amplitude.x,
        //         bubbleMovementRanges.amplitude.y,
        //         temperature);
        //
        //     Bubble.CurrentTargetRotationChangeRange = Vector2.Lerp(
        //         bubbleMovementRanges.minTargetRotationChangeRange,
        //         bubbleMovementRanges.maxTargetRotationChangeRange,
        //         temperature);
        //
        //     //if (currentCalm != newCalm)
        //     //{
        //     //    if (newCalm)
        //     //    {
        //     //        Bubble.CurrentTimeBetweenChangeTargetDirection = 0.5f;
        //     //        Bubble.CurrentRotationSpeed = 0.03f;
        //     //        Bubble.CurrentTargetRotationChangeRange = new Vector2(-30, 30);
        //     //        Bubble.CurrentSpeed = 0.5f;
        //     //    }
        //     //    else
        //     //    {
        //     //        Bubble.CurrentTimeBetweenChangeTargetDirection = 0.1f;
        //     //        Bubble.CurrentRotationSpeed = 1f;
        //     //        Bubble.CurrentTargetRotationChangeRange = new Vector2(-180, 180);
        //     //        Bubble.CurrentSpeed = 2f;
        //     //    }
        //
        //     //    currentCalm = newCalm;
        //     //}
        // }
        //
        // private void Pressure()
        // {
        //     for (int i = bubbles.Count - 1; i >= 0; i--)
        //     {
        //         if (100 - bubbles[i].Oxygen < pressure)
        //             DivideBubble(bubbles[i]);
        //     }
        // }
        //
        // private void DivideBubble(Bubble bubble)
        // {
        //     Vector3 position = bubble.transform.position;
        //
        //     float random = Random.Range(0, 1);
        //     Vector3 direction = new Vector3(random, 1 - random);
        //
        //     Bubble childBubble1 = Instantiate(bubble, position + direction * spawnChildrenDistanceFromCenter, Quaternion.identity, transform);
        //     Bubble childBubble2 = Instantiate(bubble, position - direction * spawnChildrenDistanceFromCenter, Quaternion.identity, transform);
        //
        //     float newOxygenLevel = bubble.Oxygen / 2;
        //     Vector3 scale = Vector3.one * (newOxygenLevel * 0.01f);
        //
        //     childBubble1.Oxygen = newOxygenLevel;
        //     childBubble2.Oxygen = newOxygenLevel;
        //
        //     childBubble1.transform.localScale = scale;
        //     childBubble2.transform.localScale = scale;
        //
        //     childBubble1.OnTryMerge += ChildBubble_OnTryMerge;
        //     childBubble2.OnTryMerge += ChildBubble_OnTryMerge;
        //
        //     bubbles.Add(childBubble1);
        //     bubbles.Add(childBubble2);
        //
        //     bubbles.Remove(bubble);
        //     Destroy(bubble.gameObject);
        // }
        //
        // private void ChildBubble_OnTryMerge(Bubble sender, Bubble collidedBubble)
        // {
        //     if (sender.Oxygen >= maxOxygenForOneBubble)
        //         return;
        //
        //     Bubble parentBubble = Instantiate(sender, (sender.transform.position + collidedBubble.transform.position) / 2, Quaternion.identity, transform);
        //     
        //     float newOxygenLevel = sender.Oxygen * 2;
        //
        //     parentBubble.Oxygen = newOxygenLevel;
        //
        //     parentBubble.transform.localScale = Vector3.one * newOxygenLevel * 0.01f;
        //
        //     sender.OnTryMerge -= ChildBubble_OnTryMerge;
        //     collidedBubble.OnTryMerge -= ChildBubble_OnTryMerge;
        //
        //     bubbles.Add(parentBubble);
        //
        //     bubbles.Remove(sender);
        //     bubbles.Remove(collidedBubble);
        //
        //     Destroy(sender.gameObject);
        //     Destroy(collidedBubble.gameObject);
        // }
    }  
}
