using CaptainNemo.Controls;
using System.Collections.Generic;
using CaptainNemo.SeaCreature;
using UnityEngine;

namespace CaptainNemo.Game
{
	public enum NameEvent : byte
	{
		Startfish,
		Octopus
	}

	public class EventManager : MonoBehaviour
	 {
	 //    [SerializeField] private StarfishSpawner starfishSpawner = default;
		// [SerializeField] private InkSplatManager octupusSpawner = default;
	 //    [SerializeField] private float timeToTriggeredEvent;
	 //    [SerializeField] private List<NameEvent> allEvent = new List<NameEvent>();
		// [SerializeField] private List<ControlHandler> controlHandlers = new List<ControlHandler>();
	 //
	 //    private float counterTimerEvent = 0f;
	 //    private List<NameEvent> disponibleEvent = new List<NameEvent>();
	 //
 	// 	private void Start()
 	// 	{
	 //         disponibleEvent = allEvent;
		// 	 octupusSpawner.onFinished += OctupusSpawner_onFinished;
 	// 	}
	 //
		// private void OctupusSpawner_onFinished(InkSplatManager sender)
		// {
		// 	disponibleEvent.Add(NameEvent.Octopus);
		// }
	 //
		// private void Update()
	 //     {
	 //         UpdateTriggerEvent();
	 //     }
	 //
	 //     private void UpdateTriggerEvent()
 	// 	{
 	// 		if (disponibleEvent.Count == 0)
 	// 			return;
	 //
 	// 		counterTimerEvent += Time.deltaTime;
	 //
 	// 		if (counterTimerEvent > timeToTriggeredEvent)
 	// 		{
 	// 			int randomIndex = Random.Range(0, disponibleEvent.Count);
 	// 			NameEvent actualEvent = disponibleEvent[randomIndex];
	 //
 	// 			if (actualEvent == NameEvent.Startfish)
 	// 			{
 	// 				Starfish starfish = starfishSpawner.SpawnStarfish(ChoseControlToBlock());
 	// 				disponibleEvent.Remove(actualEvent);
 	// 				starfish.onStarfishDeath += Starfish_onStarfishDeath;
	 //
 	// 			}
 	// 			else if (actualEvent == NameEvent.Octopus)
 	// 			{
		// 			octupusSpawner.SpawnSplats();
 	// 				disponibleEvent.Remove(actualEvent);
 	// 			}
	 //
 	// 			counterTimerEvent = 0;
 	// 		}
 	// 	}
	 //
 	// 	private void Starfish_onStarfishDeath(Starfish sender)
 	// 	{
	 //         disponibleEvent.Add(NameEvent.Startfish);
 	// 	}
	 //
 	// 	public void IncreaseDificulty(float newTimeToTriggerEvent)
 	// 	{
 	// 		timeToTriggeredEvent = newTimeToTriggerEvent;
 	// 	}
	 //
 	// 	private ControlHandler ChoseControlToBlock()
	 //     {
	 //         int randomControl = Random.Range(0, controlHandlers.Count);
	 //         ControlHandler actualControlhandler = controlHandlers[randomControl];
	 //         return actualControlhandler;
	 //     }
	 }
}
