using System;
using CaptainNemo.Controls;
using CaptainNemo.Controls.Logic;
using UnityEngine;

namespace CaptainNemo.Player
{
    public struct GlobalParam
    {
        public float InitValue;
        public float Value;
        public Vector2 Range;
        public float VariationRate;
        public float InputVariationRate;
    }

    public class Diver: MonoBehaviour
    {
        [field: SerializeField] public PressureControl PressureControl { get; private set; }
        [field: SerializeField] public TemperatureControl TemperatureControl { get; private set; }
        [field: SerializeField] public OxygenControl OxygenControl { get; private set; }
        [field: SerializeField] public WiperControl WiperControl { get; private set; }

        [field: SerializeField] public DiverMouth Mouth { get; private set; }

        private bool IsDead;
        
        [SerializeField]
        private ParametersVariationStrategy parametersVariation;

        public GlobalParam OxygenParam;
        public GlobalParam TemperatureParam;
        public GlobalParam PressureParam;
        
        private void Start()
        {
            parametersVariation = Instantiate(parametersVariation);
            InitGlobalParams(parametersVariation);
        }

        private void InitGlobalParams(ParametersVariationStrategy parameters)
        {
            OxygenParam = new GlobalParam()
            {
                InitValue = parameters.InitOxygen,
                Range = parameters.OxygenRange,
                Value = parameters.InitOxygen,
            };
            TemperatureParam = new GlobalParam()
            {
                InitValue = parameters.InitTemperature,
                Range = parameters.TemperatureRange,
                Value = parameters.InitTemperature,
            };
            PressureParam = new GlobalParam()
            {
                InitValue = parameters.InitPressure,
                Range = parameters.PressureRange,
                Value = parameters.InitPressure,
            };
        }

		private void Update()
        {
            if (IsDead) return;
		}

        private void LateUpdate()
        {
            UpdateGlobalParameters(parametersVariation, Time.realtimeSinceStartup, Time.deltaTime);
            Death();
        }
        
        private void UpdateGlobalParameters(ParametersVariationStrategy parameters, float elapsedTime, float deltaTime)
        {
            OxygenParam.VariationRate = parameters.GetOxygenIncreaseRate(elapsedTime) + OxygenParam.InputVariationRate;
            TemperatureParam.VariationRate = parameters.GetTemperatureIncreaseRate(elapsedTime) + TemperatureParam.InputVariationRate;
            PressureParam.VariationRate = parameters.GetPressureIncreaseRate(elapsedTime) + PressureParam.InputVariationRate;
            
            OxygenParam.Value = Mathf.Clamp(OxygenParam.Value + OxygenParam.VariationRate * deltaTime, OxygenParam.Range.x, OxygenParam.Range.y);
            TemperatureParam.Value = Mathf.Clamp(TemperatureParam.Value + TemperatureParam.VariationRate * deltaTime, TemperatureParam.Range.x, TemperatureParam.Range.y);
            PressureParam.Value = Mathf.Clamp(PressureParam.Value + PressureParam.VariationRate * deltaTime, PressureParam.Range.x, PressureParam.Range.y);

            OxygenParam.InputVariationRate = 0;
            TemperatureParam.InputVariationRate = 0;
            PressureParam.InputVariationRate = 0;
        }

        // Should be in game manager
        public void IncreaseDifficulty(float newDifficultyVariationParameter)
        {
            parametersVariation.difficultyVariationParameter = newDifficultyVariationParameter;
        }
        
        public void Oxygen(float value)
        {
            OxygenParam.InputVariationRate += value;
        }

        public void Temperature(float value)
        {
            Debug.Log($"Temperature variation {value}");
            TemperatureParam.InputVariationRate += value;
        }

        public void Pressure(float value)
        {
            PressureParam.InputVariationRate += value;
        }

        private void Death()
        {
            IsDead = OxygenParam.Value <= 0;
            if (IsDead)
            {
                SoundManager.PlaySound(E_Sound.Death);

                Debug.Log("T'es mort gros naze");
            }
        }
    }
}