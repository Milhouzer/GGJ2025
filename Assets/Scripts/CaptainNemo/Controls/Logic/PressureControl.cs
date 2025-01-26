using CaptainNemo.Game;
using UnityEngine;
using UnityEngine.Serialization;

namespace CaptainNemo.Controls.Logic
{
    /// <summary>
    /// Temperature control handler. This component is attached to the temperature valve
    /// </summary>
    public class PressureControl : ControlHandler
    {
        /// <summary>
        /// Control input attenuation
        /// </summary>
        [SerializeField] private float attenuation = 0.5f;

        /// <summary>
        /// Current pressure value.
        /// </summary>
        private float _pressure;

        /// <summary>
        /// This component controls the pressure
        /// </summary>
        [SerializeField] private GlobalControlParam globalParam = GlobalControlParam.Pressure;
        
        /// <summary>
        /// Processes control input by accumulating pressure.
        /// </summary>
        /// <param name="value">Input control vector.</param>
        protected override void OnControl(Vector2 value)
        {
            Debug.Log($"PressureControl {value.y * attenuation}");
            GameManager.AddPressure(value.y * attenuation);
            _pressure = value.y * attenuation;
        }
        
        /// <summary>
        /// Get the actual pressure of the control
        /// </summary>
        /// <returns></returns>
        public override float GetControlValue()
        {
            return _pressure;
        }

        /// <summary>
        /// Controls the pressure
        /// </summary>
        /// <returns></returns>
        public override GlobalControlParam GetGlobalControlParam()
        {
            return globalParam;
        }
    }
}