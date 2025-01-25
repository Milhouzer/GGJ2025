using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubblesManager : MonoBehaviour
{
	public float BubbleMoveRadius = 0.4f;

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


	private void OnDrawGizmosSelected()
	{
		
	}
	
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

        TestChangeBubbleMovement();
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

	public void DestroyBubble(Bubble bubbleToDestroy)
	{
		allBubbles.Remove(bubbleToDestroy);
		Destroy(bubbleToDestroy.gameObject);
	}

 #region Test
    private bool currentCalm = true;
    [SerializeField] private bool newCalm = true;

    private void TestChangeBubbleMovement()
    {
        if (currentCalm != newCalm)
        {
            if (newCalm)
            {
                foreach (var bubble in allBubbles)
                {
                    bubble.CurrentTimeBetweenChangeTargetDirection = 0.5f;
                    bubble.CurrentRotationSpeed = 0.03f;
                    bubble.CurrentTargetRotationChangeRange = new Vector2(-30, 30);
                    bubble.CurrentSpeed = 0.5f;
                }
            }
            else
            {
                foreach (var bubble in allBubbles)
                {
                    bubble.CurrentTimeBetweenChangeTargetDirection = 0.1f;
                    bubble.CurrentRotationSpeed = 1f;
                    bubble.CurrentTargetRotationChangeRange = new Vector2(-180, 180);
                    bubble.CurrentSpeed = 2f;
                }
            }

            currentCalm = newCalm;
        }
    }
    #endregion
}
