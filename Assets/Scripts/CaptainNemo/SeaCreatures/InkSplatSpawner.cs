using System.Collections.Generic;
using CaptainNemo.Controls;
using CaptainNemo.Game;
using UnityEngine;

namespace CaptainNemo.SeaCreatures
{

    public class InkSplatSpawner : MonoBehaviour
    {
        [SerializeField] private InkSplat inkSplatPrefab;
        [SerializeField] private Vector2 randomSpawn;
        private float _spawnTimer;

        private IControlHandler _wiperControl;
        public static InkSplat CurrentInkSplat;

        private void Start()
        {
            IControlHandler wiperControl = GameManager.GetWiperControl();
            if (wiperControl != null)
            {
                _wiperControl = wiperControl;
            };
			
            _spawnTimer = Random.Range(randomSpawn.x, randomSpawn.y);
        }
        

        private void Update()
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <= 0)
            {
                _spawnTimer = Random.Range(randomSpawn.x, randomSpawn.y);
                TrySpawnInkSplat();
            }
        }

        public bool TrySpawnInkSplat()
        {
            if (CurrentInkSplat != null)
            {
                Debug.Log("[InkSplat spawner] Trying to spawn an inksplat that is locked.");
                return false;
            }

            SoundManager.PlaySound(E_Sound.OctopusSquirt);
            CurrentInkSplat = Instantiate(inkSplatPrefab, _wiperControl.LockTransform());
            
            return true;
        }
    }
}
