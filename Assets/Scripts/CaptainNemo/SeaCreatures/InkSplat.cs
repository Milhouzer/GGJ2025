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
            splat.color = Color.Lerp(splat.color, Color.clear, 1f - _currentWipeAmount/wipeAmountRequired);
            if (_currentWipeAmount >= wipeAmountRequired)
            {
                Destroy(gameObject);
            }
        }
    }
}