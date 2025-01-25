using UnityEngine;

namespace CaptainNemo.Controls
{
    /// <summary>
    /// Example implementation of a control handler that tracks pressure.
    /// </summary>
    public class PressureControl : ControlHandler
    {
        /// <summary>
        /// Current pressure value.
        /// </summary>
        [SerializeField] private float _pressure;

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
        public override void Control(Vector2 value)
        {
            _pressure += value.y;
        }
        
        /// <summary>
        /// Get the actual pressure of the control
        /// </summary>
        /// <returns></returns>
        public override float GetControlValue()
        {
            return _pressure;
        }
    }
}