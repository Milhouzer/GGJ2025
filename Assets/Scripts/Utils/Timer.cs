using CaptainNemo.Bubbles;
using UnityEngine;

namespace Utils
{
    public class Timer : Bubble
    {
        [SerializeField] private float lifetime = 1f;
    
        private float elapsedTime;

        protected virtual void Update()
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= lifetime)
                Expire();
        }

        private void Expire()
        {
            // Null check for safety
            if (BubblesManager.Instance) {
                BubblesManager.Instance.allBubbles.Remove(this);
            }

            Destroy(gameObject);
        }

        // Optional: Allow external reset of timer
        public void ResetTimer()
        {
            elapsedTime = 0f;
        }
    }
}