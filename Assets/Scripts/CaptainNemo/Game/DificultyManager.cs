using CaptainNemo.Bubbles;
using UnityEngine;
using CaptainNemo.Player;

namespace CaptainNemo.Game
{
	public class DificultyManager : MonoBehaviour
	{
		[SerializeField] private AllGameDificulty allGameDificulty;
		
		private int index = 0;
		private float counterIncreaseDificulty = 0;
		private GameDificulty actualGameDificulty;
		
		private void Start()
		{
			AttributeParameter();	
		}
		
		private void AttributeParameter()
		{
			actualGameDificulty = allGameDificulty.allGameDificulty[index];
		
			BubblesManager.Instance.IncreaseDificulty(actualGameDificulty.weightBadBubble, actualGameDificulty.bubbleSpawnXSeoncds,
				actualGameDificulty.weightedIncrementBadBubble);
		
			// eventManager.IncreaseDificulty(actualGameDificulty.timeToTriggerEvent);
			GameManager.GetDiver()?.IncreaseDifficulty(actualGameDificulty.dificultyVariationParameter);
		}
		
		private void IncreaseDificulty()
		{
			index++;
			AttributeParameter();
		}
		
		private void Update()
		{
			if (index >= allGameDificulty.allGameDificulty.Count - 1)
				return;
            
			counterIncreaseDificulty += Time.deltaTime;
		
			if(counterIncreaseDificulty > actualGameDificulty.timeToChangeDificulty)
			{
				IncreaseDificulty();
				counterIncreaseDificulty = 0;
			}
		}
	}
}

