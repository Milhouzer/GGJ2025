using System;
using CaptainNemo.Game;
using UnityEngine;

namespace CaptainNemo.Controls.Logic
{
    /// <summary>
    /// Temperature control handler. This component is attached to the temperature valve
    /// </summary>
    public class TemperatureControl : ControlHandler
    {
        /// <summary>
        /// Control input attenuation
        /// </summary>
        [SerializeField] private float attenuation = 0.5f;
        
        /// <summary>
        /// Current pressure value.
        /// </summary>
        private float _temperature;
    
        /// <summary>
        /// This component controls the temperature
        /// </summary>
        [SerializeField] private GlobalControlParam globalParam = GlobalControlParam.Temperature;


        [SerializeField] private SpriteRenderer handleRenderer;
        [SerializeField] private Sprite handleA;
        [SerializeField] private Sprite handleB;
        [SerializeField] private GameObject actionRequired;

        private void Update()
        {
            actionRequired.SetActive(GameManager.GetTemperature() >= GameManager.GetMaxTemperatureValue() * 0.8f);
        }

        /// <summary>
        /// Processes control input by accumulating pressure.
        /// </summary>
        /// <param name="value">Input control vector.</param>
        protected override void OnControl(Vector2 value)
        {
            GameManager.AddTemperature(value.y * attenuation);
            handleRenderer.sprite = handleRenderer.sprite == handleA ? handleB : handleA;
            _temperature = value.y * attenuation;
        }
        
        /// <summary>
        /// Get the actual pressure of the control
        /// </summary>
        /// <returns></returns>
        public override float GetControlValue()
        {
            return _temperature;
        }
        
        /// <summary>
        /// Controls the oxygen level
        /// </summary>
        /// <returns></returns>
        public override GlobalControlParam GetGlobalControlParam()
        {
            return globalParam;
        }
    }
}