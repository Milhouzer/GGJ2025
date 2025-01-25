using UnityEngine;

namespace CaptainNemo.Controls.Logic
{
    /// <summary>
    /// Temperature control handler. This component is attached to the temperature valve
    /// </summary>
    public class TemperatureControl : ControlHandler
    {
        /// <summary>
        /// Current pressure value.
        /// </summary>
        private float _temperature;

        /// <summary>
        /// This component controls the temperature
        /// </summary>
        [SerializeField] private GlobalControlParam globalParam = GlobalControlParam.Temperature;

        /// <summary>
        /// Custom logic when control is acquired.
        /// </summary>
        protected override void OnHandle()
        {
            // Custom acquisition logic specific to this control type
            
            // For example, the starfish leg may override this function to break and immediately release control after
            // as it shouldn't exist anymore.
            
            // Implement visual effects, sounds, etc.
        }

        /// <summary>
        /// Custom logic when control is released.
        /// </summary>
        protected override void OnRelease()
        {
            // Custom release logic specific to this control type
            
            // Implement on release business logic: visual effects, sounds, etc.
        }

        /// <summary>
        /// Processes control input by accumulating pressure.
        /// </summary>
        /// <param name="value">Input control vector.</param>
        protected override void OnControl(Vector2 value)
        {
            _temperature = Mathf.Clamp(_temperature + value.y, clampValue.x, clampValue.y);
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