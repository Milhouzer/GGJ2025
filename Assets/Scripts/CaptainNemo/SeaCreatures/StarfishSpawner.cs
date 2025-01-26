using System;
using System.Collections.Generic;
using CaptainNemo.Controls;
using CaptainNemo.Game;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CaptainNemo.SeaCreatures
{
	public class StarfishSpawner : MonoBehaviour
	{
		[SerializeField] private Starfish starfishPrefab;
		[SerializeField] private float spawnRate = 5f;
		private float spawnTimer = 0f;
		
		/// <summary>
		/// Spawnables handlers
		/// </summary>
		private List<IControlHandler> handlers = new List<IControlHandler>();

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

		private void Start()
		{
			IControlHandler pressureControl = GameManager.GetPressureControl();
			if (pressureControl != null)
			{
				handlers.Add(pressureControl);
			};
			IControlHandler temperatureControl = GameManager.GetTemperatureControl();
			if (temperatureControl != null)
			{
				handlers.Add(temperatureControl);
			};
			
			spawnTimer = spawnRate;
		}

		private void Update()
		{
			spawnTimer -= Time.deltaTime;
			if (spawnTimer <= 0)
			{
				spawnTimer = spawnRate;
				if (TrySpawnStarfish(out Starfish starfish))
				{
					Debug.Log($"[Starfish spawner] Spawned {starfish}.");
				}
			}
		}

		public bool TrySpawnStarfish(out Starfish spawned)
		{
			int index = Random.Range(0, handlers.Count);
			IControlHandler spawnControl = handlers[index];
			if (spawnControl.IsLocked())
			{
				Debug.Log("[Starfish spawner] Trying to spawn a starfish that is locked.");
				spawned = null;
				return false;
			}

			spawned = Instantiate(starfishPrefab, spawnControl.LockTransform());
			spawned.OnStarfishDeath += sender =>
			{
				handlers[index].Unlock();
			};
			spawnControl.Lock();
			return true;
		}
	}
}

