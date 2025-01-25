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

        /// <summary>
        /// Processes control input by accumulating pressure.
        /// </summary>
        /// <param name="value">Input control vector.</param>
        protected override void OnControl(Vector2 value)
        {
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