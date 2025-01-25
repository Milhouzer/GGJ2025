using System.Collections.Generic;
using CaptainNemo.Controls;
using UnityEngine;

namespace CaptainNemo.Starfish
{
    public class StarfishSpawner : MonoBehaviour
    {
        [SerializeField]private Transform starfishPrefab;
        private Dictionary<Transform, ControlHandler> starfishToBlockedInteractible = new Dictionary<Transform, ControlHandler>();

        public void SpawnStarfish(ControlHandler toBlock)
        {
            Transform spawnedStarfishTransform = Instantiate(starfishPrefab, toBlock.transform.position, Quaternion.identity);
            starfishToBlockedInteractible.Add(spawnedStarfishTransform, toBlock);
            global::CaptainNemo.Starfish.Starfish spawnedStarfish = spawnedStarfishTransform.GetComponent<global::CaptainNemo.Starfish.Starfish>();
            spawnedStarfish.onStarfishDeath += OnStarfishDeath;
            toBlock.isBlockingControls = true;
        }

        private void OnStarfishDeath(global::CaptainNemo.Starfish.Starfish sender)
        {
            starfishToBlockedInteractible[sender.transform].isBlockingControls = false;
            sender.onStarfishDeath -= OnStarfishDeath;
            starfishToBlockedInteractible.Remove(sender.transform);
        }
    }
}
