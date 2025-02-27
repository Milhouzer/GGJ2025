using CaptainNemo.Player;
using System.Collections.Generic;
using CaptainNemo.Game;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CaptainNemo.Bubbles
{
	public class BubblesManager : MonoBehaviour
	{
		[SerializeField] public float bubbleMoveRadius = 4.5f;

		[SerializeField] private Bubble.BubbleMovementRanges bubbleMovementRanges = default;
		[SerializeField] private float spawnChildrenDistanceFromCenter = 0.5f;
		[Space(5)]
		[SerializeField] private WeightedBubble badBubble;
		[SerializeField] private WeightedBubble goodBubble;
		[SerializeField] private float weightedIncrement = default;
	    [SerializeField] private Vector2 rangeRandomAngle = default;
	    [SerializeField] private Transform origin;
		public static BubblesManager Instance { get; private set; }

		public List<Bubble> allBubbles = new();

		private float counterSpawnBubble = 0;
	    private float randomAngle = 0;
	    private float previousRandomAngle = default;
		private float spawnXSeconds = default;
		
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

	        MoveBubbles();
	        UpdateParameters();
	    }

	    private void MoveBubbles()
	    {
		    allBubbles.ForEach(bubble =>
		    {
			    bubble.transform.position += new Vector3(Mathf.Cos(bubble.currentDirectionAngle), Mathf.Sin(bubble.currentDirectionAngle)) * (CurrentAmplitude * Time.deltaTime);

			    bubble.currentDirectionAngle = Mathf.MoveTowards(bubble.currentDirectionAngle, bubble.targetDirectionAngle, CurrentRotationSpeed);
			    if (Vector3.Distance(bubble.transform.position, origin.position) > bubbleMoveRadius)
			    {
				    bubble.transform.position = origin.position + bubble.transform.position.normalized * bubbleMoveRadius;
				    bubble.targetDirectionAngle = Mathf.Atan2(-bubble.transform.position.y, -bubble.transform.position.x);
				    bubble.currentDirectionAngle = bubble.targetDirectionAngle;
			    }
		    });
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
				
				if (newBubble.Oxygen <= 0)
				{
					newBubble.Oxygen = Random.Range(-10f, -Bubble.MaxBubbleOxygen);
					badBubble.weight = weightedIncrement;
				}
				else
				{
					newBubble.Oxygen = Random.Range(10f, Bubble.MaxBubbleOxygen);
					badBubble.weight += weightedIncrement;
				}
				newBubble.gameObject.SetActive(true);

				allBubbles.Add(newBubble);
				badBubble.weight += newBubble.Oxygen >= 0 ? weightedIncrement : startWeightBubble;
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

		[field: SerializeField] public float CurrentTimeBetweenChangeTargetDirection { get; set; } = 0.5f;
		[field: SerializeField] public float CurrentRotationSpeed { get; set; } = 0.03f;
		[field: SerializeField] public Vector2 CurrentTargetRotationChangeRange { get; set; } = new Vector2(-30f, 30f);
		[field: SerializeField] public float CurrentAmplitude { get; set; } = 0.5f;
		
		private void Temperature()
		{
			float temperature = GameManager.GetTemperature() * 0.01f;

			CurrentTimeBetweenChangeTargetDirection = Mathf.Lerp(
				bubbleMovementRanges.timeBetweenChangeTargetDirection.x,
				bubbleMovementRanges.timeBetweenChangeTargetDirection.y,
				temperature);

			CurrentRotationSpeed = Mathf.Lerp(
				bubbleMovementRanges.rotationSpeed.x,
				bubbleMovementRanges.rotationSpeed.y,
				temperature);

			CurrentAmplitude = Mathf.Lerp(
				bubbleMovementRanges.amplitude.x,
				bubbleMovementRanges.amplitude.y,
				temperature);

			CurrentTargetRotationChangeRange = Vector2.Lerp(
				bubbleMovementRanges.minTargetRotationChangeRange,
				bubbleMovementRanges.maxTargetRotationChangeRange,
				temperature);

			// Parameters set
			
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
				if (100 - allBubbles[i].Oxygen < GameManager.GetPressure())
					DivideBubble(allBubbles[i]);
			}
		}

		private void DivideBubble(Bubble bubble)
		{
            return;
			SoundManager.PlaySound(E_Sound.BubbleDivide);

			Vector3 position = bubble.transform.position;

			float random = Random.Range(0, 1);
			Vector3 direction = new Vector3(random, 1 - random);

			Bubble childBubble1 = Instantiate(bubble, position + direction * spawnChildrenDistanceFromCenter, Quaternion.identity, transform);
			Bubble childBubble2 = Instantiate(bubble, position - direction * spawnChildrenDistanceFromCenter, Quaternion.identity, transform);

			float newOxygenLevel = bubble.Oxygen / 2;
			Vector3 scale = Vector3.one * Mathf.Abs(newOxygenLevel);

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
			if (sender.Oxygen >= Bubble.MaxBubbleOxygen)
				return;

			SoundManager.PlaySound(E_Sound.BubbleMerge);

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
