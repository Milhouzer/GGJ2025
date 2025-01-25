using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CaptainNemo.Bubbles
{
	public class BubblesManager : MonoBehaviour
	{
		public const float BUBBLE_MOVE_RADIUS = 4.5f;

		[SerializeField] private WeightedBubble badBubble;
		[SerializeField] private WeightedBubble goodBubble;
		[SerializeField] private float weightedIncrement = default;
	    [SerializeField] private Vector2 rangeRandomAngle = default;
	    [SerializeField] private Transform origin;
		public static BubblesManager Instance { get; private set; }

	    private float counterSpawnBubble = 0;
	    private float randomAngle = 0;
	    private float previousRandomAngle = default;
		private float spawnXSeconds = default;
		
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

		public void IncreaseDificulty(float newWeightBadBubble, float newSpawnXSeconds, float newWeightIncrement)
		{
			if(startWeightBubble != badBubble.weight)
				badBubble.weight = newWeightBadBubble + (badBubble.weight - startWeightBubble);
			else
				badBubble.weight = newWeightBadBubble;

			spawnXSeconds = newSpawnXSeconds;
			weightedIncrement = newWeightIncrement;
			startWeightBubble = newWeightBadBubble;
		}

	    void Update()
	    {
	        counterSpawnBubble += Time.deltaTime;

	        if (counterSpawnBubble >= spawnXSeconds)
	        {
	            SpawnBubble();
	            counterSpawnBubble = 0;
	            previousRandomAngle = randomAngle;
	        }

	        TestChangeBubbleMovement();
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

				if (newBubble is BadBubble)
				{
					newBubble.Oxygen = newBubble.Oxygen * -1;
					badBubble.weight = weightedIncrement;
				}
				else
					badBubble.weight += weightedIncrement;

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
			Destroy(bubbleToDestroy.gameObject);
		}

	 #region Test
	    private bool currentCalm = true;
	    [SerializeField] private bool newCalm = true;

	    private void TestChangeBubbleMovement()
	    {
	        //if (currentCalm != newCalm)
	        //{
	        //    if (newCalm)
	        //    {
	        //        foreach (var bubble in allBubbles)
	        //        {
	        //            bubble.CurrentTimeBetweenChangeTargetDirection = 0.5f;
	        //            bubble.CurrentRotationSpeed = 0.03f;
	        //            bubble.CurrentTargetRotationChangeRange = new Vector2(-30, 30);
	        //            bubble.CurrentSpeed = 0.5f;
	        //        }
	        //    }
	        //    else
	        //    {
	        //        foreach (var bubble in allBubbles)
	        //        {
	        //            bubble.CurrentTimeBetweenChangeTargetDirection = 0.1f;
	        //            bubble.CurrentRotationSpeed = 1f;
	        //            bubble.CurrentTargetRotationChangeRange = new Vector2(-180, 180);
	        //            bubble.CurrentSpeed = 2f;
	        //        }
	        //    }

	        //    currentCalm = newCalm;
	        //}
	    }
	    #endregion
	}

}
