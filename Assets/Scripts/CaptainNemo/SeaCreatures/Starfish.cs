using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CaptainNemo.SeaCreature
{
    public delegate void OnStarfishDeathEventHandler(Starfish sender);
    public class Starfish : MonoBehaviour
    {
        public event OnStarfishDeathEventHandler OnStarfishDeath;
    
        [SerializeField] private List<Tentacle> tentacles;
        [SerializeField] private float tentacleShakeStrength;

        private int _tentaclesCount;
        
        private void OnValidate()
        {
            _tentaclesCount = tentacles.Count;
        }

        private void Start()
        {
            _tentaclesCount = tentacles.Count;
            tentacles.ForEach(t => t.OnDie += sender =>
            {
                this._tentaclesCount--;

                transform.DOShakePosition(0.3f, 0.5f, 30, 70f, false);
                
                if (this._tentaclesCount <= 0)
                {
                    Die();
                }
            });
        }
        

        private void Die()
        {
            OnStarfishDeath?.Invoke(this);
            Destroy(gameObject);
        }
    }
}