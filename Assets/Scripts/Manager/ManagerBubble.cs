using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBubble : MonoBehaviour
{
	[SerializeField] private WeightedBubble badBubble;
	[SerializeField] private WeightedBubble goodBubble;
	[SerializeField] private float weightedIncrement = default;
    [SerializeField] private float radiusToSpawn = 50;
    [SerializeField] private float minRadiusToSpawn = 10;
    [SerializeField] private Vector2 rangeRandomAngle = default;
    [SerializeField] private float defaultSpawnNSecond = default;
    [SerializeField] private Transform origin = default;
    [SerializeField] private List<float> timeToChangeLevel = new List<float>();
    [SerializeField] private List<float> allSpawnNSeconds = new List<float>();

	public static ManagerBubble Instance { get; private set; }

	private float counterSpawnBubble = 0;
    private int index = 0;
    private float counterSwitchLevels = 0;
    private float randomAngle = 0;
    private float previousRandomAngle = default;
	private float spawnNSeconds = default;
	private List<Bubble> allBubbles = new List<Bubble>();
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
			float randomRadius = Mathf.Clamp(Random.Range(0f, radiusToSpawn),minRadiusToSpawn,99999999999999999999f);

			Vector2 pointOnCircle = new Vector2(randomRadius * Mathf.Sin(randomAngle), randomRadius * Mathf.Cos(randomAngle));
			Vector2 originPosition = origin.position;
			Vector2 position = originPosition + pointOnCircle;

			Quaternion rotation = Quaternion.identity;

			Bubble bubble = ChoseBubble();

			if(bubble.CurrentBubbleSize >= 0)
				badBubble.weight += weightedIncrement;
			else
				badBubble.weight = startWeightBubble;
				
			allBubbles.Add(Instantiate(bubble, position, rotation));
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

	public void DestroyBubble(Bubble bubbleToDestroy)
	{
		allBubbles.Remove(bubbleToDestroy);
		Destroy(bubbleToDestroy);
	}
}
