using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBubble : MonoBehaviour
{
    [SerializeField] private Bubble bubble;
    [SerializeField] private float radiusToSpawn = 50;
    [SerializeField] private float minRadiusToSpawn = 10;
    [SerializeField] private Vector2 rangeRandomAngle = default;
    [SerializeField] private float defaultSpawnNSecond = default;
    [SerializeField] private Transform origin;
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

			allBubbles.Add(Instantiate(bubble, position, rotation));
		}
		else
		{
			SpawnBubble();
		}
	}
	
	public void DestroyBubble(Bubble bubbleToDestroy)
	{
		allBubbles.Remove(bubbleToDestroy);
		Destroy(bubbleToDestroy);
	}
}
