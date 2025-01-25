using System;
using System.Collections.Generic;
using CaptainNemo.Controls;
using UnityEngine;

public class StarfishSpawner : MonoBehaviour
{
    [SerializeField]private Transform starfishPrefab;
    private Dictionary<Transform, ControlHandler> starfishToBlockedInteractible = new Dictionary<Transform, ControlHandler>();

	public static StarfishSpawner Instance { get; private set; }

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

    public Starfish SpawnStarfish(ControlHandler toBlock)
    {
        Transform spawnedStarfishTransform = Instantiate(starfishPrefab, toBlock.transform.position, Quaternion.identity);
        starfishToBlockedInteractible.Add(spawnedStarfishTransform, toBlock);
        Starfish spawnedStarfish = spawnedStarfishTransform.GetComponent<Starfish>();
        spawnedStarfish.onStarfishDeath += OnStarfishDeath;
        toBlock.isBlockingControls = true;
        return spawnedStarfish;
    }

    private void OnStarfishDeath(Starfish sender)
    {
        starfishToBlockedInteractible[sender.transform].isBlockingControls = false;
        sender.onStarfishDeath -= OnStarfishDeath;
        starfishToBlockedInteractible.Remove(sender.transform);
    }
}
