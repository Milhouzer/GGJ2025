using System;
using UnityEngine;

namespace CaptainNemo.SeaCreatures
{
    public class InkSplat : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer splat;
        [SerializeField] private float wipeAmountRequired = 50f;
        private float _currentWipeAmount;

        public void Wipe(float amount)
        {
            _currentWipeAmount += amount;
            Color c = splat.color;
            c.a = 1f - _currentWipeAmount/wipeAmountRequired;
            splat.color = c;
            if (_currentWipeAmount >= wipeAmountRequired)
            {
                Destroy(gameObject);
            }
        }
    }
}