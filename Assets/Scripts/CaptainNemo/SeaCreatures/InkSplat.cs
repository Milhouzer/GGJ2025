using UnityEngine;

namespace CaptainNemo.SeaCreatures
{
    public delegate void InkSplatErased(InkSplat sender);
    
    public class InkSplat : MonoBehaviour
    {
        public event InkSplatErased OnInkSplatErased;

        [SerializeField] private float wipeAmountRequired = 50f;
        
        public void Wipe(float amount)
        {
            wipeAmountRequired -= amount;
            
            if (wipeAmountRequired <= 0)
            {
                Destroy(gameObject);
                OnInkSplatErased?.Invoke(this);
            }
        }
    }
}