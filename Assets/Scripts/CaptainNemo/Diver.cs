using System;
using CaptainNemo.Controls;
using UnityEngine;

namespace CaptainNemo
{
    public class Diver: MonoBehaviour
    {
        [SerializeField] private Transform mouth;
        private Vector3 _mouthStartPosition;
        
        [SerializeField] private float moveSpeed = 0.1f;
        [SerializeField] private float baseValueOxygen = 100f;

        private bool IsDead;
        
        [SerializeField]
        private ParametersVariationStrategy parametersVariation;
        
        private float oxygen;
        private float oxygenVariationRate = 0;
        
        /// <summary>
        /// Current oxygen level of the diver
        /// </summary>
        public float OxygenLevel => oxygen;
        
        private float temperature;
        private float temperatureVariationRate = 0;
        /// <summary>
        /// Current temperature level of the diver
        /// </summary>
        public float TemperatureLevel => temperature;
        
        private float pressure;
        private float pressureVariationRate = 0;
        /// <summary>
        /// Current pressure level of the diver
        /// </summary>
        public float PressureLevel => pressure;
        
        private void Start()
        {
            _mouthStartPosition = mouth.position;
            oxygen = baseValueOxygen;
            parametersVariation = Instantiate(parametersVariation);
        }
        
		private void Update()
        {
            if (IsDead) return;
            
            UpdateParameters(parametersVariation, Time.realtimeSinceStartup, out temperatureVariationRate, out pressureVariationRate, out oxygenVariationRate);
			TickParameters(Time.deltaTime);
            Death();
		}

        public void IncreaseDificulty(float newDificultyVariationParameter)
        {
            parametersVariation.dificultyVariationParameter = newDificultyVariationParameter;
        }

        private void UpdateParameters(ParametersVariationStrategy parameters, float realtimeSinceStartup, out float tempVar, out float pressureVar, out float oxygenVar)
        {
            tempVar = parameters.GetTemperatureIncreaseRate(realtimeSinceStartup);
            pressureVar = parameters.GetTemperatureIncreaseRate(realtimeSinceStartup);
            oxygenVar = -parameters.GetTemperatureIncreaseRate(realtimeSinceStartup);
        }

        private void TickParameters(float deltaTime)
        {
            temperature += temperatureVariationRate * deltaTime;
            pressure += pressureVariationRate * deltaTime;
            oxygen += oxygenVariationRate * deltaTime;
        }
        
		public void ParameterCallback(IControlHandler handler)
        {
            Debug.Log($"Parameter {handler.GetGlobalControlParam()} called with value {handler.GetControlValue()} on {this}");
            var handlerType = handler.GetGlobalControlParam();
            switch (handlerType)
            {
                case GlobalControlParam.Oxygen:
                    Oxygen(handler.GetControlValue());
                    break;
                case GlobalControlParam.Temperature:
                    Temperature(handler.GetControlValue());
                    break;
                case GlobalControlParam.Pressure:
                    Pressure(handler.GetControlValue());
                    break;
            }
        }
        
        private void Oxygen(float value)
        {
            oxygen = Mathf.Clamp(oxygen + value, parametersVariation.OxygenRange.x, parametersVariation.OxygenRange.y);
        }

        private void Temperature(float value)
        {
            temperature = Mathf.Clamp(temperature + value, parametersVariation.TemperatureRange.x, parametersVariation.TemperatureRange.y);
        }

        public void SetTemperature(float value)
        {
            Temperature(value);
        }

        private void Pressure(float value)
        {
            pressure = Mathf.Clamp(pressure + value, parametersVariation.PressureRange.x, parametersVariation.PressureRange.y);
        }

        public void MoveMouth(Vector2 move)
        {
            if (move == Vector2.zero)
            {
                mouth.position = Vector3.Lerp(mouth.position, _mouthStartPosition, moveSpeed);
                return;
            }
            
            mouth.position += new Vector3(move.x, move.y) * (moveSpeed * Time.deltaTime);
        }

        private void Death()
        {
            IsDead = oxygen <= 0;
            if (IsDead)
            {
                Debug.Log("T'es mort gros naze");
            }
        }
    }
}