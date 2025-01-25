using UnityEngine;

namespace CaptainNemo
{
    [CreateAssetMenu(fileName = "ParametersVariationStrategy", menuName = "Game/ParametersVariationStrategy")]
    public class ParametersVariationStrategy : ScriptableObject
    {
        public Vector2 TemperatureRange = new Vector2(0, 100f);
        public Vector2 PressureRange = new Vector2(0, 100f);
        public Vector2 OxygenRange = new Vector2(0, 100f);
        public float dificultyVariationParameter = 0;

        public float GetTemperatureIncreaseRate(float currentTime)
        {
            return Mathf.Pow(currentTime, 0.25f) * Mathf.Log(currentTime + 1) + Mathf.PerlinNoise(currentTime, 0) * dificultyVariationParameter;
        }
        
        public float GetPressureIncreaseRate(float currentTime)
        {
            return Mathf.Pow(currentTime, 0.25f) * Mathf.Log(currentTime + 1) + Mathf.PerlinNoise(currentTime, 0) * dificultyVariationParameter;
        }
        
        public float GetOxygenDecreaseRate(float currentTime)
        {
            return Mathf.Pow(currentTime, 0.25f) * Mathf.Log(currentTime + 1);
        }
    }
}