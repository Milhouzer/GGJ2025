using CaptainNemo;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubblesManager : MonoBehaviour
{
	public const float BUBBLE_MOVE_RADIUS = 4.5f;

    [SerializeField] private Diver diver = default;
    [SerializeField] private Bubble.BubbleMovementRanges bubbleMovementRanges = default;
    [SerializeField] private float spawnChildrenDistanceFromCenter = 0.5f;
    [SerializeField] private readonly float maxOxygenForOneBubble = 100f;
    [Space(5)]
    [SerializeField] private WeightedBubble badBubble;
	[SerializeField] private WeightedBubble goodBubble;
	[SerializeField] private float weightedIncrement = default;
    [SerializeField] private Vector2 rangeRandomAngle = default;
    [SerializeField] private float defaultSpawnNSecond = default;
    [SerializeField] private Transform origin;
    [SerializeField] private List<float> timeToChangeLevel = new List<float>();
    [SerializeField] private List<float> allSpawnNSeconds = new List<float>();
	public static BubblesManager Instance { get; private set; }

    private float counterSpawnBubble = 0;
    private int index = 0;
    private float counterSwitchLevels = 0;
    private float randomAngle = 0;
    private float previousRandomAngle = default;
	private float spawnNSeconds = default;
	private List<Bubble> allBubbles = new List<Bubble>();
	
	
	[SerializeField] public Vector2 spawnRange;
	private float startWeightBubble = 0;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
	}

	private void Start()
	{
		spawnNSeconds = defaultSpawnNSecond;
		startWeightBubble = badBubble.weight;
	}

    void Update()
    {
        counterSpawnBubble += Time.deltaTime;
        counterSwitchLevels += Time.deltaTime;

        SwitchLevel();

        if (counterSpawnBubble >= spawnNSeconds)
        {
            SpawnBubble();
            counterSpawnBubble = 0;
            previousRandomAngle = randomAngle;
        }
    }

	private void SwitchLevel()
	{
		counterSwitchLevels += Time.deltaTime;
		if (timeToChangeLevel.Count == 0) return;
		
		if(counterSwitchLevels >= timeToChangeLevel[index] && index < timeToChangeLevel.Count - 1)
		{
			spawnNSeconds = allSpawnNSeconds[index];
			counterSwitchLevels = 0;
			index++;
		}
	}

    private void SpawnBubble()
    {
        randomAngle = Random.Range(0f, 360f);

		if (randomAngle > previousRandomAngle + rangeRandomAngle.y || randomAngle < previousRandomAngle + rangeRandomAngle.x)
		{
			float randomRadius = Random.Range(spawnRange.x, spawnRange.y);

			Vector2 pointOnCircle = new Vector2(randomRadius * Mathf.Sin(randomAngle), randomRadius * Mathf.Cos(randomAngle));
			Vector3 position = origin.position + new Vector3(pointOnCircle.x, pointOnCircle.y, 0);

			Quaternion rotation = Quaternion.identity;

			Bubble newBubble = Instantiate(ChoseBubble(), position, rotation, transform);
			allBubbles.Add(newBubble);
			badBubble.weight += newBubble.Oxygen >= 0 ? weightedIncrement : startWeightBubble;
			Debug.Log($"Instantiated new bubble: {newBubble.transform.position}");
			newBubble.gameObject.SetActive(true);
		}
		else
		{
			SpawnBubble();
		}
	}

	private Bubble ChoseBubble()
	{
		float totalWeight = 0;

		totalWeight += badBubble.weight;
		totalWeight += goodBubble.weight;

		float randomValue = Random.Range(0, totalWeight);

		float cumulativeWeight = 0;

		List<WeightedBubble> allWeightedBubble = new List<WeightedBubble>();
		allWeightedBubble.Add(badBubble);
		allWeightedBubble.Add(goodBubble);

		foreach (var weightedBubble in allWeightedBubble)
		{
			cumulativeWeight += weightedBubble.weight;

			if (randomValue <= cumulativeWeight)
			{
				return weightedBubble.bubble;
			}
		}

		return null;
	}

    #region Bubble parameters
    public void UpdateParameters()
    {
        Temperature();

        Pressure();
    }

    private void Temperature()
    {
       float temperature = diver.TemperatureLevel * 0.01f;

        Bubble.CurrentTimeBetweenChangeTargetDirection = Mathf.Lerp(
            bubbleMovementRanges.timeBetweenChangeTargetDirection.x,
            bubbleMovementRanges.timeBetweenChangeTargetDirection.y,
            temperature);

        Bubble.CurrentRotationSpeed = Mathf.Lerp(
            bubbleMovementRanges.rotationSpeed.x,
            bubbleMovementRanges.rotationSpeed.y,
            temperature);

        Bubble.CurrentAmplitude = Mathf.Lerp(
            bubbleMovementRanges.amplitude.x,
            bubbleMovementRanges.amplitude.y,
            temperature);

        Bubble.CurrentTargetRotationChangeRange = Vector2.Lerp(
            bubbleMovementRanges.minTargetRotationChangeRange,
            bubbleMovementRanges.maxTargetRotationChangeRange,
            temperature);

        //if (currentCalm != newCalm)
        //{
        //    if (newCalm)
        //    {
        //        Bubble.CurrentTimeBetweenChangeTargetDirection = 0.5f;
        //        Bubble.CurrentRotationSpeed = 0.03f;
        //        Bubble.CurrentTargetRotationChangeRange = new Vector2(-30, 30);
        //        Bubble.CurrentSpeed = 0.5f;
        //    }
        //    else
        //    {
        //        Bubble.CurrentTimeBetweenChangeTargetDirection = 0.1f;
        //        Bubble.CurrentRotationSpeed = 1f;
        //        Bubble.CurrentTargetRotationChangeRange = new Vector2(-180, 180);
        //        Bubble.CurrentSpeed = 2f;
        //    }

        //    currentCalm = newCalm;
        //}
    }

    private void Pressure()
    {
        for (int i = allBubbles.Count - 1; i >= 0; i--)
        {
            if (100 - allBubbles[i].Oxygen < diver.PressureLevel)
                DivideBubble(allBubbles[i]);
        }
    }

    private void DivideBubble(Bubble bubble)
    {
        Vector3 position = bubble.transform.position;

        float random = Random.Range(0, 1);
        Vector3 direction = new Vector3(random, 1 - random);

        Bubble childBubble1 = Instantiate(bubble, position + direction * spawnChildrenDistanceFromCenter, Quaternion.identity, transform);
        Bubble childBubble2 = Instantiate(bubble, position - direction * spawnChildrenDistanceFromCenter, Quaternion.identity, transform);

        float newOxygenLevel = bubble.Oxygen / 2;
        Vector3 scale = Vector3.one * Mathf.Abs(newOxygenLevel) * 0.01f;

        childBubble1.Oxygen = newOxygenLevel;
        childBubble2.Oxygen = newOxygenLevel;

        childBubble1.transform.localScale = scale;
        childBubble2.transform.localScale = scale;

        childBubble1.OnTryMerge += ChildBubble_OnTryMerge;
        childBubble2.OnTryMerge += ChildBubble_OnTryMerge;

        allBubbles.Add(childBubble1);
        allBubbles.Add(childBubble2);

        allBubbles.Remove(bubble);
        Destroy(bubble.gameObject);
    }

    private void ChildBubble_OnTryMerge(Bubble sender, Bubble collidedBubble)
    {
        if (sender.Oxygen >= maxOxygenForOneBubble)
            return;

        Bubble parentBubble = Instantiate(sender, (sender.transform.position + collidedBubble.transform.position) / 2, Quaternion.identity, transform);

        float newOxygenLevel = sender.Oxygen * 2;

        parentBubble.Oxygen = newOxygenLevel;

        parentBubble.transform.localScale = Vector3.one * newOxygenLevel * 0.01f;

        sender.OnTryMerge -= ChildBubble_OnTryMerge;
        collidedBubble.OnTryMerge -= ChildBubble_OnTryMerge;

        allBubbles.Add(parentBubble);

        allBubbles.Remove(sender);
        allBubbles.Remove(collidedBubble);

        Destroy(sender.gameObject);
        Destroy(collidedBubble.gameObject);
    }
    #endregion

    public void DestroyBubble(Bubble bubbleToDestroy)
	{
		allBubbles.Remove(bubbleToDestroy);
		Destroy(bubbleToDestroy.gameObject);
	}
}
