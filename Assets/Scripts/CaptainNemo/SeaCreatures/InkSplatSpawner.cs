using System.Collections.Generic;
using CaptainNemo.Controls;
using CaptainNemo.Game;
using UnityEngine;

namespace CaptainNemo.SeaCreatures
{

    public class InkSplatSpawner : MonoBehaviour
    {
        [SerializeField] private InkSplat inkSplatPrefab;
        [SerializeField] private float spawnRate = 5f;
        private float _spawnTimer = 0f;

        private IControlHandler _wiperControl;
        public static InkSplat CurrentInkSplat;

        private void Start()
        {
            IControlHandler wiperControl = GameManager.GetWiperControl();
            if (wiperControl != null)
            {
                _wiperControl = wiperControl;
            };
			
            _spawnTimer = spawnRate;
        }
        

        private void Update()
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <= 0)
            {
                _spawnTimer = spawnRate;
                if (TrySpawnInkSplat())
                {
                    // CurrentInkSplat.OnInkSplatErased += sender =>
                    // {
                    //     Destroy(CurrentInkSplat);
                    //     CurrentInkSplat = null;
                    // };
                }
            }
        }

        public bool TrySpawnInkSplat()
        {
            if (CurrentInkSplat != null)
            {
                Debug.Log("[InkSplat spawner] Trying to spawn an inksplat that is locked.");
                return false;
            }
            
            CurrentInkSplat = Instantiate(inkSplatPrefab, _wiperControl.LockTransform());
            
            return true;
        }
    }
}
