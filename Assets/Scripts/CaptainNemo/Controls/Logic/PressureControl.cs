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
        /// Current pressure value.
        /// </summary>
        private float _pressure;

        /// <summary>
        /// This component controls the pressure
        /// </summary>
        [SerializeField] private GlobalControlParam globalParam = GlobalControlParam.Pressure;
        
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
            _pressure = Mathf.Clamp(_pressure + value.y, clampValue.x, clampValue.y);
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