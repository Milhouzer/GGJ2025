using CaptainNemo;
using CaptainNemo.Controls;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum NameEvent : byte
{
    Startfish,
    Octopus
}

public class EventManager : MonoBehaviour
{
    [SerializeField] private StarfishSpawner starfishSpawner = default;
    [SerializeField] private Diver diver = default;
    [SerializeField] private float timeToTriggeredEvent;
    [SerializeField] private List<NameEvent> allEvent = new List<NameEvent>();
    [SerializeField] private List<ControlHandler> controlHandlers = new List<ControlHandler>();

    private float counterTemperature = 0f;
    private float counterPression = 0f;
    private float counterTimerEvent = 0f;
    private List<NameEvent> disponibleEvent = new List<NameEvent>();

	private void Start()
	{
        disponibleEvent = allEvent;
	}

	private void Update()
    {
        counterTimerEvent += Time.deltaTime;

        if (counterTimerEvent > timeToTriggeredEvent)
        {
            int randomIndex = Random.Range(0, disponibleEvent.Count);
            NameEvent actualEvent = disponibleEvent[randomIndex];

            if(actualEvent == NameEvent.Startfish)
            {
                Starfish starfish = starfishSpawner.SpawnStarfish(ChoseControlToBlock());
                disponibleEvent.Remove(actualEvent);
				starfish.onStarfishDeath += Starfish_onStarfishDeath;
            }else if (actualEvent == NameEvent.Octopus)
            {

            }
        }
    }

	private void Starfish_onStarfishDeath(Starfish sender)
	{
        disponibleEvent.Add(NameEvent.Startfish);
	}

	private ControlHandler ChoseControlToBlock()
    {
        int randomControl = Random.Range(0, controlHandlers.Count);
        ControlHandler actualControlhandler = controlHandlers[randomControl];

        return null;
    }
}
