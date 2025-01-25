using UnityEngine;

namespace CaptainNemo
{
    [CreateAssetMenu(fileName = "ParametersVariationStrategy", menuName = "Game/ParametersVariationStrategy")]
    public class ParametersVariationStrategy : ScriptableObject
    {
        public float GetTemperatureIncreaseRate(float currentTime)
        {
            return Mathf.Pow(currentTime, 0.25f) * Mathf.Log(currentTime + 1);
        }
        
        public float GetPressureIncreaseRate(float currentTime)
        {
            return Mathf.Pow(currentTime, 0.25f) * Mathf.Log(currentTime + 1);
        }
        
        public float GetOxygenDecreaseRate(float currentTime)
        {
            return Mathf.Pow(currentTime, 0.25f) * Mathf.Log(currentTime + 1);
        }
    }
}